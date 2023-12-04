﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvvmLessons.Infrastructure.Commands.Base
{
    internal class LambdaCommand : Command
    {
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;

        public LambdaCommand(Action<object> Execute, Func<object,bool> CanExecute =null)
        {
            execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
            canExecute = CanExecute;
        }

        public override bool CanExecute(object? parameter) => canExecute?.Invoke(parameter) ?? true;

        public override void Execute(object? parameter) => execute(parameter);
    }
}
