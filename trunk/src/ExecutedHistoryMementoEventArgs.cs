/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;

namespace PaintDotNet
{
    public class ExecutedHistoryMementoEventArgs
        : EventArgs
    {
        private HistoryMemento newHistoryMemento;

        public HistoryMemento NewHistoryMemento
        {
            get
            {
                return this.newHistoryMemento;
            }
        }

        public ExecutedHistoryMementoEventArgs(HistoryMemento newHistoryMemento)
        {
            this.newHistoryMemento = newHistoryMemento;
        }
    }
}
