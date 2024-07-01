using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gpusoft.Tools.Msfs.AddonsManager.Helpers;

[System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
sealed class CollectionAttribute : Attribute
{
    readonly string collectionName;

    public CollectionAttribute(string collectionName)
    {
        this.collectionName = collectionName;
    }

    public string CollectionName
    {
        get
        {
            return collectionName;
        }
    }
}

sealed class MissingAttributeException : Exception
{
    public Type AttributeType
    {
        get; set;
    }

    public MissingAttributeException(Type attributeType)
    {
        AttributeType = attributeType;
    }
}
