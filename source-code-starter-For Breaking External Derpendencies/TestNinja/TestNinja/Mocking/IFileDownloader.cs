namespace TestNinja.Mocking
{
    public interface IFileDownloader
    {
        void DownloadFile(string url, string path);
        // Sometimes for many external dependencies, there can be an interfacpresent, 
        // So, in that case  you dont need to make another class and then it's interface, you can directly inject that and use it.
    }
}