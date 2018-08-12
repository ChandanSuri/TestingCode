using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace TestNinja.Mocking
{
    public class VideoService
    {
        /*
        public string ReadVideoTitle()
        {
            //var str = File.ReadAllText("video.txt"); // This is an external dependency, thus, we need to isolate this in a different class...
            var str = new FileReader().Read("video.txt"); // This still depends on FieleReader object, so, its made as an interface for making it's usage as a test double while unit testing...
            // We need to use Dependency Injection
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video.";
            return video.Title;
        }
        */

        #region Dependency Injection Via Method Parameters
        
        public string ReadVideoTitle(IFileReader fileReader) // Dependency Injection via Method Parameters
        {
            //var str = File.ReadAllText("video.txt"); // This is an external dependency, thus, we need to isolate this in a different class...
            var str = fileReader.Read("video.txt"); 
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video.";
            return video.Title;
        }

        #endregion Dependency Injection Via Method Parameters

        #region Dependency Injection Via Properties
        /*
        public IFileReader FileReader { get; set; }

        public VideoService()
        {
            FileReader = new FileReader(); // We use Real File Reader Object
        }

        public string ReadVideoTitle() // Dependency Injection via Property
        {
            //var str = File.ReadAllText("video.txt"); // This is an external dependency, thus, we need to isolate this in a different class...
            var str = FileReader.Read("video.txt");
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video.";
            return video.Title;
        }
        */
        #endregion Dependency Injection Via Properties

        #region Dependency Injection Via Constructor Params
        private IFileReader _fileReader { get; set; }

        public VideoService()// We will use this in our production code, so, that the code never breaks...
        {
            _fileReader = new FileReader();
        }
        public VideoService(IFileReader fileReader) // We would use this for Fake File Reader while Testing
        {
            _fileReader = fileReader;
        }

        /* // Better Approach
        // We can merge above 2 constructors into one as well...
        public VideoService(IFileReader fileReader = null)
        {
            _fileReader = fileReader ?? new FileReader();
        }
        */

        public string ReadVideoTitle() // Dependency Injection via Constructor Params
        {
            var str = _fileReader.Read("video.txt");
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video.";
            return video.Title;
        }

        #endregion Dependency Injection Via Constructor Params

        public string GetUnprocessedVideosAsCsv()
        {
            var videoIds = new List<int>();
            
            using (var context = new VideoContext())
            {
                var videos = 
                    (from video in context.Videos
                    where !video.IsProcessed
                    select video).ToList();
                
                foreach (var v in videos)
                    videoIds.Add(v.Id);

                return String.Join(",", videoIds);
            }
        }
    }

    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }
    }

    public class VideoContext : DbContext
    {
        public DbSet<Video> Videos { get; set; }
    }
}