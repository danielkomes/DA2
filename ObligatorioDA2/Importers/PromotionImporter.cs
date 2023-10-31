using IBusinessLogic;
using System.Reflection;

namespace Importers
{
    public class PromotionImporter : IPromotionImporter
    {
        public IEnumerable<PromotionAbstract> ImportPromotions()
        {
            IEnumerable<PromotionAbstract> ret = new List<PromotionAbstract>();
            string path = "./ImportedPromotions";
            string[] filePaths = Directory.GetFiles(path);

            foreach (string filePath in filePaths)
            {
                if (filePath.EndsWith(".dll"))
                {
                    FileInfo fileInfo = new FileInfo(filePath);
                    Assembly assembly = Assembly.LoadFile(fileInfo.FullName);

                    foreach(Type type in assembly.GetTypes())
                    {
                        if(typeof(PromotionAbstract).IsAssignableFrom(type) && !type.IsInterface)
                        {
                            PromotionAbstract promotion = (PromotionAbstract)Activator.CreateInstance(type);
                            if(promotion != null)
                            {
                                ret = ret.Append(promotion);
                            }
                        }
                    }
                }
            }
            return ret;
        }
    }
}
