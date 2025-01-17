﻿using System.Xml.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using Mielek.Azure.ApiManagement.PolicyToolkit.Authoring;

namespace Mielek.Azure.ApiManagement.PolicyToolkit.Compilation.Policy;

public class JsonPCompiler : IMethodPolicyHandler
{
    public string MethodName => nameof(IOutboundContext.JsonP);

    public void Handle(ICompilationContext context, InvocationExpressionSyntax node)
    {
        var arguments = node.ArgumentList.Arguments;
        if (arguments.Count != 1)
        {
            context.ReportError($"Wrong argument count for jsonp policy. {node.GetLocation()}");
            return;
        }

        var value = node.ArgumentList.Arguments[0].Expression.ProcessParameter(context);
        context.AddPolicy(new XElement("jsonp", new XAttribute("callback-parameter-name", value)));
    }
}