using System.Reflection;

namespace MIOTO.APPLICATION;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}