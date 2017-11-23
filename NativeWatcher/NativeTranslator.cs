namespace NativeWatcher
{
    using System;
    using System.IO;
    using System.Xml;
    using System.Linq;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    internal static class NativeTranslator
    {
        public static readonly string NativesXmlFilePath = Path.GetFullPath(@"Plugins\NativeWatcher.Natives.xml");

        static readonly Dictionary<ulong, string> originalToName;
        static readonly Dictionary<ulong, ulong> currentToOriginal;
        static readonly Dictionary<ulong, ulong> addressToCurrent;

        static unsafe NativeTranslator()
        {
            if (!File.Exists(NativesXmlFilePath))
            {
                throw new FileNotFoundException("Missing 'NativeWatcher.Natives.xml' file.");
            }

            Xml.Natives n;
            XmlSerializer serializer = new XmlSerializer(typeof(Xml.Natives));
            using (StreamReader reader = new StreamReader(NativesXmlFilePath))
            {
                n = (Xml.Natives)serializer.Deserialize(reader);
            }

            // build originalToName dictionary
            originalToName = new Dictionary<ulong, string>(n.OriginalToName.Length);
            foreach (Xml.NativesOriginalToNameTableEntry e in n.OriginalToName)
            {
                originalToName.Add(UInt64.Parse(e.Original, System.Globalization.NumberStyles.HexNumber), e.Name);
            }

            // build currentToOriginal dictionary
            int gameVersion = Rage.Game.BuildNumber;
            Xml.NativesToOriginalTable table = n.ToOriginal.FirstOrDefault(t => t.Version == gameVersion);
            if(table == null)
            {
                throw new InvalidOperationException($"Missing native hashes translation for version v{gameVersion}.");
            }

            currentToOriginal = new Dictionary<ulong, ulong>(table.Entries.Length);
            foreach (Xml.NativesToOriginalTableEntry e in table.Entries)
            {
                currentToOriginal.Add(UInt64.Parse(e.Current, System.Globalization.NumberStyles.HexNumber), UInt64.Parse(e.Original, System.Globalization.NumberStyles.HexNumber));
            }

            // build addressToCurrent dictionary
            addressToCurrent = new Dictionary<ulong, ulong>();
            NativeRegistration** nativesTable = NativeRegistration.GetRegistrationTable();
            for (int i = 0; i < 255; i++)
            {
                NativeRegistration* t = nativesTable[i];
                for (; t != null; t = t->Next)
                {
                    for (uint k = 0; k < t->EntriesCount; k++)
                    {
                        if (!addressToCurrent.ContainsKey(t->HandlersPointers[k]))
                        {
                            addressToCurrent.Add(t->HandlersPointers[k], t->Hashes[k]);
                        }
                    }
                }
            }
        }


        public static string OriginalToName(ulong originalHash)
        {
            if (originalToName.TryGetValue(originalHash, out string name))
            {
                return name;
            }
            else
            {
                //throw new InvalidOperationException($"Missing name for original hash {originalHash.ToString("X16")}");
                return "0x" + originalHash.ToString("X16");
            }
        }

        public static ulong AddressToOriginal(ulong address)
        {
            if(addressToCurrent.TryGetValue(address, out ulong current))
            {
                if(currentToOriginal.TryGetValue(current, out ulong original))
                {
                    return original;
                }
                else
                {
                    //throw new InvalidOperationException($"Missing original hash for current hash {current.ToString("X16")}");
                    return current;
                }
            }
            else
            {
                //throw new InvalidOperationException($"Missing current hash for address {address.ToString("X16")}");
                return address;
            }
        }
    }


    namespace Xml
    {
        public class Natives
        {
            [XmlArrayItem(ElementName = "Entry")] public NativesOriginalToNameTableEntry[] OriginalToName { get; set; }
            [XmlArrayItem(ElementName = "Table")] public NativesToOriginalTable[] ToOriginal { get; set; }
        }

        public class NativesOriginalToNameTableEntry
        {
            [XmlAttribute] public string Original { get; set; }
            [XmlAttribute] public string Name { get; set; }
        }

        public class NativesToOriginalTable
        {
            [XmlAttribute] public int Version { get; set; }
            [XmlArrayItem(ElementName = "Entry")] public NativesToOriginalTableEntry[] Entries { get; set; }
        }

        public class NativesToOriginalTableEntry
        {
            [XmlAttribute] public string Current { get; set; }
            [XmlAttribute] public string Original { get; set; }
        }
    }
}
