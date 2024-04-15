namespace Pr_3.ModuleTask1
{
    class Content
    {
        private string content;

        public Content(string content)
        {
            this.content = content;
        }

        public void Show()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Content: " + content);
            Console.ResetColor();
        }
    }
}
