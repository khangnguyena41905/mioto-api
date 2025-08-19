using System.Reflection;

namespace MIOTO.CONTRACT;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}