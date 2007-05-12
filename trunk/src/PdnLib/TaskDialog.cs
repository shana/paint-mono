/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using PaintDotNet.SystemLayer;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PaintDotNet
{
    public static class TaskDialog
    {
        public static int DefaultPixelWidth96Dpi = 300;

        public static TaskButton Show(
            IWin32Window owner,
            Icon formIcon,
            string formTitle,
            Image taskImage,
            string introText,
            TaskButton[] taskButtons,
            TaskButton acceptTaskButton,
            TaskButton cancelTaskButton)
        {
            return Show(owner, formIcon, formTitle, taskImage, true, introText,
                taskButtons, acceptTaskButton, cancelTaskButton);
        }

        public static TaskButton Show(
            IWin32Window owner,
            Icon formIcon,
            string formTitle,
            Image taskImage,
            bool scaleTaskImageWithDpi,
            string introText,
            TaskButton[] taskButtons,
            TaskButton acceptTaskButton,
            TaskButton cancelTaskButton)
        {
            return Show(owner, formIcon, formTitle, taskImage, scaleTaskImageWithDpi, introText, 
                taskButtons, acceptTaskButton, cancelTaskButton, DefaultPixelWidth96Dpi);
        }

        public static TaskButton Show(
            IWin32Window owner,
            Icon formIcon,
            string formTitle,
            Image taskImage,
            bool scaleTaskImageWithDpi,
            string introText,
            TaskButton[] taskButtons,
            TaskButton acceptTaskButton,
            TaskButton cancelTaskButton,
            int pixelWidth96Dpi)
        {
            using (TaskDialogForm form = new TaskDialogForm())
            {
                form.Icon = formIcon;
                form.IntroText = introText;
                form.Text = formTitle;
                form.TaskImage = taskImage;
                form.ScaleTaskImageWithDpi = scaleTaskImageWithDpi;
                form.TaskButtons = taskButtons;
                form.AcceptTaskButton = acceptTaskButton;
                form.CancelTaskButton = cancelTaskButton;

                int pixelWidth = UI.ScaleWidth(pixelWidth96Dpi);
                form.ClientSize = new Size(pixelWidth, form.ClientSize.Height);

                DialogResult dr = form.ShowDialog(owner);
                TaskButton result = form.DialogResult;

                return result;
            }
        }

        private sealed class TaskDialogForm
            : PdnBaseForm
        {
            private PictureBox taskImagePB;
            private bool scaleTaskImageWithDpi;
            private Label introTextLabel;
            private TaskButton[] taskButtons;
            private CommandLink[] commandLinks;
            private HeaderLabel separator;
            private TaskButton acceptTaskButton;
            private TaskButton cancelTaskButton;
            private TaskButton dialogResult;

            public new TaskButton DialogResult
            {
                get
                {
                    return this.dialogResult;
                }
            }

            public Image TaskImage
            {
                get
                {
                    return this.taskImagePB.Image;
                }

                set
                {
                    this.taskImagePB.Image = value;
                    PerformLayout();
                    Invalidate(true);
                }
            }

            public bool ScaleTaskImageWithDpi
            {
                get
                {
                    return this.scaleTaskImageWithDpi;
                }

                set
                {
                    this.scaleTaskImageWithDpi = value;
                    PerformLayout();
                    Invalidate(true);
                }
            }

            public string IntroText
            {
                get
                {
                    return this.introTextLabel.Text;
                }

                set
                {
                    this.introTextLabel.Text = value;
                    PerformLayout();
                    Invalidate(true);
                }
            }

            public TaskButton[] TaskButtons
            {
                get
                {
                    return (TaskButton[])this.taskButtons.Clone();
                }

                set
                {
                    this.taskButtons = (TaskButton[])value.Clone();
                    InitCommandLinks();
                    PerformLayout();
                    Invalidate(true);
                }
            }

            public TaskButton AcceptTaskButton
            {
                get
                {
                    return this.acceptTaskButton;
                }

                set
                {
                    this.acceptTaskButton = value;

                    IButtonControl newAcceptButton = null;

                    for (int i = 0; i < this.commandLinks.Length; ++i)
                    {
                        TaskButton asTaskButton = this.commandLinks[i].Tag as TaskButton;

                        if (this.acceptTaskButton == asTaskButton)
                        {
                            newAcceptButton = this.commandLinks[i];
                        }
                    }

                    AcceptButton = newAcceptButton;
                }
            }

            public TaskButton CancelTaskButton
            {
                get
                {
                    return this.cancelTaskButton;
                }

                set
                {
                    this.cancelTaskButton = value;

                    IButtonControl newCancelButton = null;

                    for (int i = 0; i < this.commandLinks.Length; ++i)
                    {
                        TaskButton asTaskButton = this.commandLinks[i].Tag as TaskButton;

                        if (this.cancelTaskButton == asTaskButton)
                        {
                            newCancelButton = this.commandLinks[i];
                        }
                    }

                    CancelButton = newCancelButton;
                }
            }

            public TaskDialogForm()
            {
                InitializeComponent();
            }

            private void InitializeComponent()
            {
                SuspendLayout();
                this.introTextLabel = new Label();
                this.taskImagePB = new PictureBox();
                this.separator = new HeaderLabel();
                //
                // introTextLabel
                //
                this.introTextLabel.Name = "introTextLabel";
                //
                // taskImagePB
                //
                this.taskImagePB.Name = "taskImagePB";
                this.taskImagePB.SizeMode = PictureBoxSizeMode.StretchImage;
                //
                // separator
                //
                this.separator.Name = "separator";
                this.separator.RightMargin = 0;
                //
                // TaskDialogForm
                //
                this.Name = "TaskDialogForm";
                this.ClientSize = new Size(300, 100);
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.MinimizeBox = false;
                this.MaximizeBox = false;
                this.ShowInTaskbar = false;
                this.StartPosition = FormStartPosition.CenterParent;
                this.Controls.Add(this.introTextLabel);
                this.Controls.Add(this.taskImagePB);
                this.Controls.Add(this.separator);
                ResumeLayout();
            }

            protected override void OnLayout(LayoutEventArgs levent)
            {
                int leftMargin = UI.ScaleWidth(8);
                int rightMargin = UI.ScaleWidth(8);
                int topMargin = UI.ScaleHeight(8);
                int bottomMargin = UI.ScaleHeight(8);
                int imageToIntroHMargin = UI.ScaleWidth(8);
                int topSectionToLinksVMargin = UI.ScaleHeight(8);
                int commandLinkVMargin = UI.ScaleHeight(0);
                int afterCommandLinksVMargin = UI.ScaleHeight(8);
                int insetWidth = ClientSize.Width - leftMargin - rightMargin;

                if (this.taskImagePB.Image == null)
                {
                    this.taskImagePB.Location = new Point(0, topMargin);
                    this.taskImagePB.Size = new Size(0, 0);
                    this.taskImagePB.Visible = false;
                }
                else
                {
                    this.taskImagePB.Location = new Point(leftMargin, topMargin);

                    if (this.scaleTaskImageWithDpi)
                    {
                        this.taskImagePB.Size = UI.ScaleSize(this.taskImagePB.Image.Size);
                    }
                    else
                    {
                        this.taskImagePB.Size = this.taskImagePB.Image.Size;
                    }

                    this.taskImagePB.Visible = true;
                }

                this.introTextLabel.Location = new Point(this.taskImagePB.Right + imageToIntroHMargin, this.taskImagePB.Top);
                this.introTextLabel.Width = ClientSize.Width - this.introTextLabel.Left - rightMargin;
                this.introTextLabel.Height = this.introTextLabel.GetPreferredSize(new Size(this.introTextLabel.Width, 1)).Height;

                int y = Math.Max(this.taskImagePB.Bottom, this.introTextLabel.Bottom);
                y += topSectionToLinksVMargin;

                if (this.commandLinks != null)
                {
                    this.separator.Location = new Point(leftMargin, y);
                    this.separator.Width = insetWidth;
                    y += this.separator.Height;

                    for (int i = 0; i < this.commandLinks.Length; ++i)
                    {
                        this.commandLinks[i].Location = new Point(leftMargin, y);
                        this.commandLinks[i].Width = insetWidth;
                        this.commandLinks[i].PerformLayout();
                        y += this.commandLinks[i].Height + commandLinkVMargin;
                    }

                    y += afterCommandLinksVMargin;
                }

                this.ClientSize = new Size(ClientSize.Width, y);
                base.OnLayout(levent);
            }

            private void InitCommandLinks()
            {
                SuspendLayout();

                if (this.commandLinks != null)
                {
                    foreach (CommandLink commandLink in this.commandLinks)
                    {
                        Controls.Remove(commandLink);
                        commandLink.Tag = null;
                        commandLink.Click -= CommandLink_Click;
                        commandLink.Dispose();
                    }

                    this.commandLinks = null;
                }

                this.commandLinks = new CommandLink[this.taskButtons.Length];

                IButtonControl newAcceptButton = null;
                IButtonControl newCancelButton = null;

                for (int i = 0; i < this.commandLinks.Length; ++i)
                {
                    TaskButton taskButton = this.taskButtons[i];
                    CommandLink commandLink = new CommandLink();

                    commandLink.ActionText = taskButton.ActionText;
                    commandLink.ActionImage = taskButton.Image;
                    commandLink.AutoSize = true;
                    commandLink.ExplanationText = taskButton.ExplanationText;
                    commandLink.Tag = taskButton;
                    commandLink.Click += CommandLink_Click;

                    this.commandLinks[i] = commandLink;
                    Controls.Add(commandLink);

                    if (this.acceptTaskButton == taskButton)
                    {
                        newAcceptButton = commandLink;
                    }

                    if (this.cancelTaskButton == taskButton)
                    {
                        newCancelButton = commandLink;
                    }
                }

                AcceptButton = newAcceptButton;
                CancelButton = newCancelButton;

                ResumeLayout();
            }

            private void CommandLink_Click(object sender, EventArgs e)
            {
                CommandLink commandLink = (CommandLink)sender;
                this.dialogResult = (TaskButton)commandLink.Tag;
                Close();
            }
        }
    }
}