using Images.Implementation;
using Images.Interfaces;
using Security.Implementation;
using Security.Interfaces;

namespace ExtensionsServices.Implementation
{
    public class ServiceProvider
    {
        private static ServiceProvider _instance;

        public static ServiceProvider Instance
        {
            get { return _instance ?? (_instance = new ServiceProvider()); }
        }

        private ImageService _imageService;
        private SecurityService _securityService;

        private ServiceProvider()
        {
            _imageService = new ImageService();
            _securityService = new SecurityService();
        }

        public static IImageService Images
        {
            get { return Instance._imageService; }
        }

        public static ISecurityService Security
        {
            get { return Instance._securityService; }
        }
    }
}