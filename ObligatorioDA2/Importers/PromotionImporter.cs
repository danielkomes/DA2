using PromotionInterface;
using System.Reflection;

namespace Importers
{
    public class PromotionImporter : IPromotionImporter
    {
        public IEnumerable<PromotionAbstractModelIn> ImportPromotions()
        {
            IEnumerable<PromotionAbstractModelIn> ret = new List<PromotionAbstractModelIn>();
            string path = "../Importers/ImportedPromotions";
            string[] filePaths = Directory.GetFiles(path);

            foreach (string filePath in filePaths)
            {
                if (filePath.EndsWith(".dll"))
                {
                    FileInfo fileInfo = new FileInfo(filePath);
                    Assembly assembly = Assembly.LoadFile(fileInfo.FullName);

                    foreach(Type type in assembly.GetTypes())
                    {
                        if(typeof(PromotionAbstractModelIn).IsAssignableFrom(type) && !type.IsInterface)
                        {
                            PromotionAbstractModelIn promotion = (PromotionAbstractModelIn)Activator.CreateInstance(type);
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
