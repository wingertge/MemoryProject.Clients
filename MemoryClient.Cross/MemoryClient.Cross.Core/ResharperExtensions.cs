using System;
using JetBrains.Annotations;

namespace MemoryClient.Cross.Core
{
    public static class ResharperExtensions
    {
        [SourceTemplate]
        public static void command([Macro(Expression = "suggestVariableName()")] string propertyName, [Macro(Expression = "decapitalize()", Editable = -1)] string fieldName, [Macro(Expression = "complete()")] string initializerExpr)
        {
            //$private Lazy<$fieldType$> _$fieldName$;
            //$public ICommand $propertyName$ => _$fieldName$.Value;$END$


        }
    }
}