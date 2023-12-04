namespace Mielek.Azure.ApiManagement.PolicyToolkit.Builders.Policies
{
    using System.Collections.Immutable;
    using System.Xml.Linq;

    using Mielek.Azure.ApiManagement.PolicyToolkit.Builders.Expressions;
    using Mielek.Azure.ApiManagement.PolicyToolkit.Generators.Attributes;

    [GenerateBuilderSetters]
    public partial class RetryPolicyBuilder
    {
        private IExpression<bool>? _condition;
        private uint? _count;
        private uint? _interval;
        [IgnoreBuilderField]
        private ICollection<XElement>? _policies;
        private uint? _maxInterval;
        private uint? _delta;
        private IExpression<string>? _firstFastRetry;

        public RetryPolicyBuilder Policies(Action<PolicySectionBuilder> configurator)
        {
            var builder = new PolicySectionBuilder();
            configurator(builder);
            _policies = builder.Build();
            return this;
        }

        public XElement Build()
        {
            if (_condition == null) throw new NullReferenceException();
            if (_count == null) throw new NullReferenceException();
            if (_interval == null) throw new NullReferenceException();
            if (_policies == null) throw new NullReferenceException();

            var children = ImmutableArray.CreateBuilder<object>();
            children.Add(_condition.GetXAttribute("condition"));
            children.Add(new XAttribute("count", _count));
            children.Add(new XAttribute("interval", _interval));

            if (_maxInterval != null)
            {
                children.Add(new XAttribute("max-interval", _maxInterval));
            }
            if (_delta != null)
            {
                children.Add(new XAttribute("delta", _delta));
            }
            if (_firstFastRetry != null)
            {
                children.Add(_firstFastRetry.GetXAttribute("first-fast-retry"));
            }

            children.AddRange(_policies);

            return new XElement("retry", children.ToArray());
        }
    }
}


namespace Mielek.Azure.ApiManagement.PolicyToolkit.Builders
{
    using Mielek.Azure.ApiManagement.PolicyToolkit.Builders.Policies;

    public partial class PolicySectionBuilder
    {
        public PolicySectionBuilder Retry(Action<RetryPolicyBuilder> configurator)
        {
            var builder = new RetryPolicyBuilder();
            configurator(builder);
            _sectionPolicies.Add(builder.Build());
            return this;
        }
    }
}