namespace Pr_3.ModuleTask1
{
    class Title
    {
        private string title;

        public Title(string title)
        {
            this.title = title;
        }

        public void Show()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Title: " + title);
            Console.ResetColor();
        }
    }
}