using System.Collections.Generic;
public class RootObject
{
    public string type { get; set; }
    public Metadata metadata { get; set; }
    public List<int> bbox { get; set; }
    public List<Feature> features { get; set; }
}