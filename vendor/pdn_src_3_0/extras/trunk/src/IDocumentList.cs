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
    public enum DocumentClickAction
    {
        Select,
        Close
    }

    public interface IDocumentList
    {
        /// <summary>
        /// This event is raised when the user clicks on a Document in the list.
        /// </summary>
        event EventHandler<Pair<DocumentWorkspace, DocumentClickAction>> DocumentClicked;

        event EventHandler DocumentListChanged;

        DocumentWorkspace[] DocumentList
        {
            get;
        }

        int DocumentCount
        {
            get;
        }

        void AddDocumentWorkspace(DocumentWorkspace addMe);
        void RemoveDocumentWorkspace(DocumentWorkspace removeMe);
        void SelectDocumentWorkspace(DocumentWorkspace selectMe);
    }
}
