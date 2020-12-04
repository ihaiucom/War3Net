﻿// ------------------------------------------------------------------------------
// <copyright file="EqualsValueClauseFactory.cs" company="Drake53">
// Licensed under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>
// ------------------------------------------------------------------------------

namespace War3Net.CodeAnalysis.Jass.Syntax
{
    public static partial class JassSyntaxFactory
    {
        public static EqualsValueClauseSyntax EqualsValueClause(NewExpressionSyntax expression)
        {
            return new EqualsValueClauseSyntax(Token(SyntaxTokenType.Assignment), expression);
        }
    }
}