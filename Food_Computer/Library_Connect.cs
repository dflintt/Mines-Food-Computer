using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Helpers;
using System.Xml;


namespace Food_Computer
{
    public class Library_Connect
    { 
        public static void write(string[] args)
        {
            var address = new YamlMappingNode(
            new YamlScalarNode("Module"), new YamlScalarNode("1") { Style = YamlDotNet.Core.ScalarStyle.Literal },
            new YamlScalarNode("Plant"), new YamlScalarNode("lettuce"),
            new YamlScalarNode("fridge"), new YamlScalarNode("thefridge")
        )
            { Anchor = "main-address" };

            var stream = new YamlStream(
                new YamlDocument(
                    new YamlMappingNode(
                        new YamlScalarNode("repeipt"), new YamlScalarNode("Oz-Ware Purchase Invoice"),
                        new YamlScalarNode("date"), new YamlScalarNode("2007-08-06"),
                        new YamlScalarNode("customer"), new YamlMappingNode(
                            new YamlScalarNode("given"), new YamlScalarNode("Dorothy"),
                            new YamlScalarNode("family"), new YamlScalarNode("Gale")
                        ),
                        new YamlScalarNode("items"), new YamlSequenceNode(
                            new YamlMappingNode(
                                new YamlScalarNode("part_no"), new YamlScalarNode("A4786"),
                                new YamlScalarNode("descrip"), new YamlScalarNode("Water Bucket (Filled)"),
                                new YamlScalarNode("price"), new YamlScalarNode("1.47"),
                                new YamlScalarNode("quantity"), new YamlScalarNode("4")
                            ),
                            new YamlMappingNode(
                                new YamlScalarNode("part_no"), new YamlScalarNode("E1628"),
                                new YamlScalarNode("descrip"), new YamlScalarNode("High Heeled \"Ruby\" Slippers"),
                                new YamlScalarNode("price"), new YamlScalarNode("100.27"),
                                new YamlScalarNode("quantity"), new YamlScalarNode("1")
                            )
                        ),
                        new YamlScalarNode("bill-to"), address,
                        new YamlScalarNode("ship-to"), address,
                        new YamlScalarNode("specialDelivery"), new YamlScalarNode("Follow the Yellow Brick\nRoad to the Emerald City.\nPay no attention to the\nman behind the curtain.") { Style = YamlDotNet.Core.ScalarStyle.Literal }
                    )
                )
            );

            using (TextWriter writer = File.CreateText("C:\\temp\\test.yaml"))
                stream.Save(writer, false);
                ;

        }
 
    }
    
    
}
