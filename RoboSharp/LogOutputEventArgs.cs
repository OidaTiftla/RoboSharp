using System;

namespace RoboSharp {

    public class LogOutputEventArgs : EventArgs {
        public string LogOutput { get; set; }

        public LogOutputEventArgs(string logOutput) {
            this.LogOutput = logOutput;
        }
    }
}