using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinjaUnitTests.Mocking
{
    [TestFixture]
    class VideoServiceTests
    {
        private Mock<IFileReader> _fileReader;
        private Mock<IVideoRepository> _repository;
        private VideoService _videoService;

        [SetUp]
        public void SetUp()
        {
            _fileReader = new Mock<IFileReader>();
            _repository = new Mock<IVideoRepository>();
            _videoService = new VideoService(_fileReader.Object, _repository.Object);
        }

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            /*
            // Dependency Injection Via Method Parameters (Test)
            var service = new VideoService();
            var result = service.ReadVideoTitle(new FakeFileReader());
            Assert.That(result, Does.Contain("error").IgnoreCase);
            */

            /*
            // Dependency Injection Via Properties (Test)
            var service = new VideoService
            {
                FileReader = new FakeFileReader()
            };
            var result = service.ReadVideoTitle();
            Assert.That(result, Does.Contain("error").IgnoreCase);
            */

            /*
            // Dependency Injection Via Constructor Params
            var service = new VideoService(new FakeFileReader());
            var result = service.ReadVideoTitle(new FakeFileReader());
            Assert.That(result, Does.Contain("error").IgnoreCase);
            */

            // Using Moq for testing... (Only for dealing with external dependencies, since a lot of work)
            _fileReader.Setup(fr => fr.Read("video.txt")).Returns(""); // everything defined for the Mock File Reader...
            // fileReader is a Moq, if you want an Object of type extended from the Interface, use Object with it.
            var result = _videoService.ReadVideoTitle();
            Assert.That(result, Does.Contain("error").IgnoreCase);

        }

        // When there are some dependencies that are used only for 1-2 methods, pass them as parameters... As the case here, though not done.
        // We need 2 Tests...
        [Test]
        public void GetUnprocessedVideosAsCsv_AllVideosAreProcessed_ReturnAnEmptyString()
        {
            _repository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>());

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_AFewUnProcessedVideos_ReturnStringWithIdsOfUnprocessedVideos()
        {
            _repository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video> {
                new Video { Id = 1 },
                new Video { Id = 2 },
                new Video { Id = 3 }

            });

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo("1,2,3"));
        }
    }
}
