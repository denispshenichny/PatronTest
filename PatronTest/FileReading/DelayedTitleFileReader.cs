using System;
using System.Threading.Tasks;

namespace PatronTest.FileReading
{
    public class DelayedTitleFileReader : TitleFileReader
    {
        public override async Task<string?> ReadContentAsync(string path)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            return await base.ReadContentAsync(path);
        }
    }
}
