using DictionariesSystem.Contracts.Core.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DictionariesSystem.Framework.Core.Commands.Common
{
    public class HelpCommand : BaseCommand, ICommand
    {
        public const string ParametersNames = "";
        private const int NumberOfParameters = 0;

        protected override int ParametersCount
        {
            get
            {
                return NumberOfParameters;
            }
        }

        public override string Execute(IList<string> parameters)
        {
            base.Execute(parameters);

            var result = new StringBuilder();
            result.AppendLine();

            var currentAssembly = this.GetType().GetTypeInfo().Assembly;
            currentAssembly.DefinedTypes
                .Where(type => type.ImplementedInterfaces.Any(inter => inter == typeof(ICommand)) && type.Name != "BaseCommand" && type.Name != this.GetType().Name)
                .Select(type =>
                {
                    FieldInfo fieldInfo = type.GetFields().FirstOrDefault(f => f.Name.StartsWith("Parameters"));

                    var commandName = type.Name;
                    var commandParameters = fieldInfo.GetRawConstantValue();
                  
                    return $"{commandName.Replace("Command", string.Empty)} {commandParameters}";
                })
                .ToList()
                .ForEach(x => result.AppendLine(x));

            return result.ToString();
        }
    }
}