using System.Reflection;

namespace MIOTO.PERSISTENCE;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}