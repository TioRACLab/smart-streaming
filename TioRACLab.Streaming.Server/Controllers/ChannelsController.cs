using Microsoft.AspNetCore.Mvc;

namespace TioRACLab.Streaming.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChannelsController : BaseStreamingController
    {
        [HttpGet("")]
        public ActionResult<List<Channel>> ListChannels()
        {
            return new List<Channel>()
            {
                new Channel()
                {
                    Id = Guid.NewGuid(),
                    Name = "Channel Test",
                    Slug = "test"
                },
                new Channel()
                {
                    Id = Guid.NewGuid(),
                    Name = "New Channel Test",
                    Slug = "test2"
                },
            };
        }

        [HttpGet("{slug}")]
        public ActionResult<Channel> GetChannel(string slug)
        {
            return new Channel()
                {
                    Id = Guid.NewGuid(),
                    Name = "Channel Test",
                    Slug = slug,
                    Scenes = new List<Scene>()
                    {
                        new Scene()
                        {
                            Id= Guid.NewGuid(),
                            Name = "Scene 1",
                            Slug = "scene1"
                        },
                        new Scene()
                        {
                            Id= Guid.NewGuid(),
                            Name = "Scene 2",
                            Slug = "scene2"
                        }
                    }
                };
        }

        [HttpGet("{channelSlug}/Scene/{sceneSlug}")]
        public ActionResult<Scene> GetScene(string channelSlug, string sceneSlug)
        {
            return new Scene()
            {
                Id = Guid.NewGuid(),
                Name = "My Scene 1",
                Slug = sceneSlug
            };
        }
    }
}
