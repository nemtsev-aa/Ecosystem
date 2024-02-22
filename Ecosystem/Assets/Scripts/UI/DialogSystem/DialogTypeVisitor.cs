using System.Collections.Generic;
using System.Linq;

public class DialogTypeVisitor : IDialogTypeVisitor {
    private readonly IEnumerable<Dialog> _dialogs;

    public DialogTypeVisitor(IEnumerable<Dialog> dialogs) {
        _dialogs = dialogs;
    }

    public Dialog Dialog { get; private set; }

    public void Visit(Dialog dialog) => Visit((dynamic)dialog);

    public void Visit(DesktopDialog mainMenu) => Dialog = _dialogs.FirstOrDefault(dialog => dialog is DesktopDialog);

    public void Visit(SettingsDialog settings) => Dialog = _dialogs.FirstOrDefault(dialog => dialog is SettingsDialog);

    public void Visit(AboutDialog aboutDialog) => Dialog = _dialogs.FirstOrDefault(dialog => dialog is AboutDialog);
}
