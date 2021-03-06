﻿// ------------------------------------------------------------------------------
// <copyright file="InvocationExpressionFactory.cs" company="Drake53">
// Licensed under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>
// ------------------------------------------------------------------------------

using War3Net.CodeAnalysis.Jass.Syntax;

namespace War3Net.CodeAnalysis.Jass
{
    public static partial class JassSyntaxFactory
    {
        public static JassInvocationExpressionSyntax InvocationExpression(string name, JassArgumentListSyntax arguments)
        {
            return new JassInvocationExpressionSyntax(
                ParseIdentifierName(name),
                arguments);
        }

        public static JassInvocationExpressionSyntax InvocationExpression(string name, params IExpressionSyntax[] arguments)
        {
            return new JassInvocationExpressionSyntax(
                ParseIdentifierName(name),
                ArgumentList(arguments));
        }
    }
}