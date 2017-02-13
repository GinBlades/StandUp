using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using static System.Console;

namespace StandUp {
    /// <summary>
    /// A simple console application to alternate between standing and sitting every hour.
    /// NOTE: To get executable, remove "type" from dependencies and add runtime.
    /// Publish with 'dotnet publish -c release -r win10-x64'
    /// </summary>
    public class Program {
        public static void Main(string[] args) {
            WriteLine("Are you sitting or standing?");
            var currentState = ReadLine().Trim().ToLower();
            while(true) {
                Run(currentState);
                AlertSwitch();
                WriteLine("Ready?");
                var response = ReadLine();
                if (response == "exit") {
                    break;
                }
                SwitchPositions(ref currentState);
            }
        }

        private static void AlertSwitch() {
            var defaultColor = ForegroundColor;
            ForegroundColor = ConsoleColor.Green;
            WriteLine(new string('*', 30));
            WriteLine("Time to switch.");
            WriteLine(new string('*', 30));
            ForegroundColor = defaultColor;
        }

        private static void SwitchPositions(ref string currentState) {
            currentState = currentState == "sitting" ? "standing" : "sitting";
        }

        private static void Run(string currentState) {
            var nextPosition = currentState == "sitting" ? "stand" : "sit";
            var oneHourFromNow = DateTime.Now.AddHours(1);
            if (currentState == "sitting") {
                WriteLine($"You should stand at: {oneHourFromNow}\n");
            } else {
                WriteLine($"You should sit at: {oneHourFromNow}\n");
            }
            var secondsInHour = 60 * 60;
            for(var i = 0; i <= secondsInHour; i++) {
                Thread.Sleep(1000);
                SetCursorPosition(0, CursorTop - 1);
                WriteRemainingTime(secondsInHour - i);
                if (i == secondsInHour / 2) {
                    AlertMove();
                }
            }
        }

        // TODO: Without padding, the line doesn't clear properly.
        private static void WriteRemainingTime(int timeRemaining) {
            var minutes = timeRemaining / 60;
            var seconds = timeRemaining % 60;
            var displayMinutes = minutes.ToString().PadLeft(2, '0');
            var displaySeconds = seconds.ToString().PadLeft(2, '0');
            WriteLine($"\r{displayMinutes}:{displaySeconds} remaining");
        }

        private static void AlertMove() {
            var defaultColor = ForegroundColor;
            ForegroundColor = ConsoleColor.Cyan;
            WriteLine(new string('*', 30));
            WriteLine("Move around a little.");
            WriteLine(new string('*', 30));
            WriteLine();
            ForegroundColor = defaultColor;
        }

        private enum Position {
            Sit, Stand
        }
    }
}
