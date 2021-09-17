using System;

namespace CleanArchitectureBase.Application.Contracts
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SuppressAttribute : Attribute { }
}
