using Mielek.Azure.ApiManagement.PolicyToolkit.Authoring.Expressions;

namespace Mielek.Azure.ApiManagement.PolicyToolkit.Authoring;

public interface IOutboundContext : IHaveExpressionContext
{
    void SetHeader(string name, params string[] values);
    void SetHeaderIfNotExist(string name, params string[] values);
    void AppendHeader(string name, params string[] values);
    void RemoveHeader(string name);
    void SetBody(string body);
    void Base();
}