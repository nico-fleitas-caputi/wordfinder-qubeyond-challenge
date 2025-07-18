namespace WordFinder;

public class WordFinder : IWordFinder
{
    private readonly IEnumerable<string> _matrix;
    private readonly string _leftToRightMatrix;
    private readonly string _topToBottomMatrix;
    private const int _topNumberFromOcurrences = 10;

    /// <summary>
    /// Class for searching a Matrix to look for words.
    /// </summary>
    /// <param name="matrix"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public WordFinder(IEnumerable<string> matrix) // def per requirement
    {
        _matrix = matrix ?? throw new ArgumentNullException(nameof(matrix));
        _leftToRightMatrix = LeftToRightMatrix();
        _topToBottomMatrix = TopToBottomMatrix();
    }

    /// <summary>
    /// Method for searching a Matrix to look for words from the word stream
    /// <remarks>
    /// Per requirement words may appear horizontally from left to right 
    /// or vertically from top to bottom.
    /// </remarks>
    /// </summary>
    /// <param name="wordstream"></param>
    /// <returns></returns>
    public IEnumerable<string> Find(IEnumerable<string> wordstream) // def per requirement
    {
        List<string> result = new List<string>();

        List<WordOcurrence> wordStreamOcurrences = wordstream
            .Select(ws => new WordOcurrence()
            {
                word = ws,
                ocurrences = 0
            })
            .ToList();

        FindInString(_leftToRightMatrix, ref wordStreamOcurrences);
        FindInString(_topToBottomMatrix, ref wordStreamOcurrences);

        result = wordStreamOcurrences.Where(wpo => wpo.ocurrences > 0)
            .OrderByDescending(wpo => wpo.ocurrences)
            .Select(wpo => wpo.word)
            .Take(_topNumberFromOcurrences)
            .ToList();

        return result.ToHashSet(); // per requirement returns a Set
    }

    private string LeftToRightMatrix()
    {
        string leftToRightMatrix = string.Empty;

        foreach (string word in _matrix)
        {
            leftToRightMatrix += word;
        }

        return leftToRightMatrix;
    }

    private string TopToBottomMatrix()
    {
        List<string> matrixToList = _matrix.ToList();
        string topToBottomMatrix = string.Empty;
        int dimensionX = matrixToList.Count();
        int dimensionY = matrixToList[0].Length;

        // transpose matrix copy
        for (int x = 0; x < dimensionX; x++)
        {
            for (int y = 0; y < dimensionY; y++)
            {
                topToBottomMatrix += matrixToList[y][x];
            }
        }

        return topToBottomMatrix;
    }

    private void FindInString(string matrixToString, ref List<WordOcurrence> wordStreamOcurrences)
    {
        foreach (WordOcurrence word in wordStreamOcurrences)
        {
            word.ocurrences += AllOcurrencesOf(matrixToString, word.word);
        }
    }

    private int AllOcurrencesOf(string matrixToString, string word)
    {
        int ocurrences = 0;

        List<int> indexes = new List<int>();
        for (int i = 0; ; i += word.Length)
        {
            i = matrixToString.IndexOf(word, i);
            if (i == -1) // no more ocurrences
            {
                return ocurrences;
            }
            ocurrences++;
        }
    }
}
