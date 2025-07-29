using Aspire.Hosting;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<MarketApi>("MarketApi");
var seq = builder.AddSeq("seq")
                 .ExcludeFromManifest();

var myService = builder.AddProject<MarketApi>("MarketApi2")
                       .WithReference(seq);
builder.Build().Run();
