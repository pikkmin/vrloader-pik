using System;
using System.IO;
using System.Runtime.InteropServices;

namespace PikMod
{
	// Token: 0x02000008 RID: 8
	internal static class ConsoleUtils
	{
		// Token: 0x0600001C RID: 28
		[DllImport("kernel32.dll")]
		internal static extern int AllocConsole();

		// Token: 0x0600001D RID: 29
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool SetForegroundWindow(IntPtr hWnd);

		// Token: 0x0600001E RID: 30
		[DllImport("kernel32.dll")]
		private static extern IntPtr GetConsoleWindow();

		// Token: 0x0600001F RID: 31 RVA: 0x000028EE File Offset: 0x00000AEE
		public static void ShowConsole()
		{
			ConsoleUtils.SetForegroundWindow(ConsoleUtils.GetConsoleWindow());
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000028FC File Offset: 0x00000AFC
		public static void Load()
		{
			ConsoleUtils.AllocConsole();
			Console.SetOut(new StreamWriter(Console.OpenStandardOutput())
			{
				AutoFlush = true
			});
			Console.SetIn(new StreamReader(Console.OpenStandardInput()));
			Console.Clear();
			Console.WriteLine("Created by Pik");
			ConsoleUtils.ShowConsole();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002948 File Offset: 0x00000B48
		public static string AskInput(string question)
		{
			ConsoleUtils.ShowConsole();
			Console.Write(question);
			while (Console.KeyAvailable)
			{
				Console.ReadKey();
			}
			string text = Console.ReadLine();
			if (!(text == string.Empty))
			{
				return text;
			}
			return null;
		}
	}
}
