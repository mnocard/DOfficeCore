using DOfficeCore.Infrastructure.Commands.Core;
using System;

namespace DOfficeCore.Infrastructure.Commands
{
    internal class LambdaCommand : CommandCore
    {
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;

        public LambdaCommand(Action<object> Execute, Func<object, bool> CanExecute = null)
        {
            execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
            canExecute = CanExecute;
        }

        protected override bool CanExecute(object parameter) => canExecute?.Invoke(parameter) ?? true;

        protected override void Execute(object parameter) => execute(parameter);
    }
}
