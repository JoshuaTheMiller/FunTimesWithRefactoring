<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
	var foo = new Foo { Name = "Josh" };
	
	var fooAsString = JsonConvert.SerializeObject(foo);
	
	// This works fine.
	// If you REALLY want to do something like this, I strongly advise setting default values
	// for properties, like I did below. This will help reduce the amount of null checks 
	// needed accross your code base.
	var deserializedObject = JsonConvert.DeserializeObject<Bar>(fooAsString);
	
	deserializedObject.Dump();
}

// Define other methods and classes here
public class Foo
{
	public string Name { get; set; }
}

public class Bar
{
	public string Id { get; set; } = "Default";
}