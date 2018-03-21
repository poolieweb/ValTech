namespace ValTech
{
    using System.Collections.Generic;

    public interface IParseFileToInt
    {
        bool TryParseFileToEnumerableOfInt(string filePath, out List<int> doorNumberEnumerable);
    }
}