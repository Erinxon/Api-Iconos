using HtmlAgilityPack;
using Icon.Api.Models;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Web;
using Icon.Api.Models;
using System.Linq;

namespace Icon.Api.Services
{
    public class IconService : IIconService
    {
        private readonly XPathModel xPathModel;
        private readonly SectionUrlPage urlPage;
        private HtmlDocument htmlDocument;

        public IconService(IOptions<SectionUrlPage> urlPage, IOptions<XPathModel> xPathModel)
        {
            this.xPathModel = xPathModel.Value;
            this.urlPage = urlPage.Value;
        }

        public IconlineAwesome GetIcons()
        {
            var nodePrincipal = this.htmlDocument.DocumentNode.SelectNodes(xPathModel.XPATHGeneral);
            var icons = nodePrincipal.Select(n => n.SelectNodes(xPathModel.XPATHIcons));
            var iconsType = icons.Select(i => i.Select(n => n.SelectNodes(xPathModel.XPATHTypeIcons))
            .SingleOrDefault())
            .SingleOrDefault().Select(x =>  new IconType()
                {
                    IconTypeName = x.SelectSingleNode(xPathModel.XPATHIconTypeName).InnerText.Trim().Replace("\r\n", string.Empty),
                    IconDetails = x.SelectNodes(xPathModel.XPATHIconDetails).Select(nn => new IconDetail()
                    {
                        IconName = nn.SelectSingleNode(xPathModel.XPATHIconName).InnerText.Trim().Replace("\r\n", string.Empty),
                        IconClass = nn.SelectSingleNode(xPathModel.XPATHIconClass).GetAttributeValue(xPathModel.XPATHGetAttributeIconClass, string.Empty).Trim().Replace("\r\n", string.Empty),

                    })
            });
            var IconlineAwesome = new IconlineAwesome()
            {
                IconsType = iconsType
            };
            return IconlineAwesome;
        }

        public async Task LoadHtmlDocument()
        {
            HtmlWeb htmlWeb = new();
            HtmlDocument htmlDoc = await htmlWeb.LoadFromWebAsync($"{this.urlPage.Url}");
            htmlDoc.LoadHtml(HttpUtility.HtmlDecode(htmlDoc.DocumentNode.InnerHtml.ToString()));
            this.htmlDocument = htmlDoc;
        }
    }
}
