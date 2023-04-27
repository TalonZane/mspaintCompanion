using System;
using System.Threading;
using System.Windows.Forms;

namespace mspaintCompanion
{
    static class Program
    {
        /// <summary>
        /// An array of the default randomized export filenames.
        /// </summary>
        public readonly static string[] DefaultFileNames = new string[] {
            "awesome drawing",
            "nice work",
            "wonderful art",
            "beautiful",
            "looks great",
            "lovely",
            "stunning"
        };

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new MultiFormContext(new MainForm(), new LayerRenderer()));
        }
    }

    /// <summary>
    /// Represents an <see cref="ApplicationContext"/> instance, responsible for
    /// managing multiple concurrent open forms.
    /// </summary>
    public class MultiFormContext : ApplicationContext
    {
        private int openForms;
        public MultiFormContext(params Form[] forms)
        {
            openForms = forms.Length;

            foreach (var form in forms)
            {
                form.FormClosed += (s, args) =>
                {
                    if (Interlocked.Decrement(ref openForms) == 0) {
                        ExitThread();
                    }
                };

                form.Show();
            }
        }
    }
}
