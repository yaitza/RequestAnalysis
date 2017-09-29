namespace RequestAnalysis
{
    public class OutputShow
    {
        public delegate void ShowMessageHandler(string msg);

        public static ShowMessageHandler ShowMethod { get; set; }

        public static void ShowMessage(string msg)
        {
            if (ShowMethod != null)
            {
                ShowMethod.Invoke(msg);
            }
        }
    }
}