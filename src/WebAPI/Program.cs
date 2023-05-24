// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

await WebApplication
  .CreateBuilder(args)
  .RegisterComponents()
  .RegisterPipelines()
  .RunAsync();