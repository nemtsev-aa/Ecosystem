public interface IDialogTypeVisitor {
    void Visit(Dialog dialog);
    void Visit(DesktopDialog mainMenu);
    void Visit(SettingsDialog settings);
    void Visit(AboutDialog aboutDialog);
}
