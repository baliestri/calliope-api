// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace Calliope.WebAPI.Attributes;

/// <summary>
///   Attribute to ignore a parameter in the swagger documentation.
/// </summary>
[AttributeUsage(AttributeTargets.Parameter)]
public sealed class ParameterIgnoreAttribute : Attribute { }