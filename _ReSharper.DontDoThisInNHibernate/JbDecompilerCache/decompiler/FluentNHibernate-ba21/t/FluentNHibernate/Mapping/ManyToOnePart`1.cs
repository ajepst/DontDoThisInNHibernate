// Type: FluentNHibernate.Mapping.ManyToOnePart`1
// Assembly: FluentNHibernate, Version=1.3.0.727, Culture=neutral, PublicKeyToken=8aa435e3cb308880
// Assembly location: C:\working\opensource\DontDoThisInNHibernate\packages\FluentNHibernate.1.3.0.727\lib\FluentNHibernate.dll

using FluentNHibernate;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel;
using FluentNHibernate.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentNHibernate.Mapping
{
  public class ManyToOnePart<TOther> : IManyToOneMappingProvider
  {
    private readonly IList<string> columns = (IList<string>) new List<string>();
    private bool nextBool = true;
    private readonly AttributeStore attributes = new AttributeStore();
    private readonly AttributeStore columnAttributes = new AttributeStore();
    private readonly AccessStrategyBuilder<ManyToOnePart<TOther>> access;
    private readonly FetchTypeExpression<ManyToOnePart<TOther>> fetch;
    private readonly NotFoundExpression<ManyToOnePart<TOther>> notFound;
    private readonly CascadeExpression<ManyToOnePart<TOther>> cascade;
    private readonly Type entity;
    private readonly Member member;

    /// <summary>
    /// Set the fetching strategy
    /// 
    /// </summary>
    /// 
    /// <example>
    /// Fetch.Select();
    /// 
    /// </example>
    public FetchTypeExpression<ManyToOnePart<TOther>> Fetch
    {
      get
      {
        return this.fetch;
      }
    }

    /// <summary>
    /// Set the behaviour for when this relationship is null in the database
    /// 
    /// </summary>
    /// 
    /// <example>
    /// NotFound.Exception();
    /// 
    /// </example>
    public NotFoundExpression<ManyToOnePart<TOther>> NotFound
    {
      get
      {
        return this.notFound;
      }
    }

    /// <summary>
    /// Specifies the cascade behaviour for this relationship
    /// 
    /// </summary>
    /// 
    /// <example>
    /// Cascade.All();
    /// 
    /// </example>
    public CascadeExpression<ManyToOnePart<TOther>> Cascade
    {
      get
      {
        return this.cascade;
      }
    }

    /// <summary>
    /// Specifies the access strategy for this relationship
    /// 
    /// </summary>
    /// 
    /// <example>
    /// Access.Field();
    /// 
    /// </example>
    public AccessStrategyBuilder<ManyToOnePart<TOther>> Access
    {
      get
      {
        return this.access;
      }
    }

    /// <summary>
    /// Inverts the next boolean
    /// 
    /// </summary>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public ManyToOnePart<TOther> Not
    {
      get
      {
        this.nextBool = !this.nextBool;
        return this;
      }
    }

    public ManyToOnePart(Type entity, Member member)
    {
      this.entity = entity;
      this.member = member;
      this.access = new AccessStrategyBuilder<ManyToOnePart<TOther>>(this, (Action<string>) (value => this.attributes.Set("Access", 2, (object) value)));
      this.fetch = new FetchTypeExpression<ManyToOnePart<TOther>>(this, (Action<string>) (value => this.attributes.Set("Fetch", 2, (object) value)));
      this.cascade = new CascadeExpression<ManyToOnePart<TOther>>(this, (Action<string>) (value => this.attributes.Set("Cascade", 2, (object) value)));
      this.notFound = new NotFoundExpression<ManyToOnePart<TOther>>(this, (Action<string>) (value => this.attributes.Set("NotFound", 2, (object) value)));
      this.SetDefaultAccess();
    }

    private void SetDefaultAccess()
    {
      Access access = MemberAccessResolver.Resolve(this.member);
      if (access == Access.Property || access == Access.Unset)
        return;
      this.attributes.Set("Access", 0, (object) access.ToString());
    }

    /// <summary>
    /// Sets whether this relationship is unique
    /// 
    /// </summary>
    /// 
    /// <example>
    /// Unique();
    ///             Not.Unique();
    /// 
    /// </example>
    public ManyToOnePart<TOther> Unique()
    {
      this.columnAttributes.Set("Unique", 2, (object) (bool) (this.nextBool ? 1 : 0));
      this.nextBool = true;
      return this;
    }

    /// <summary>
    /// Specifies the name of a multi-column unique constraint.
    /// 
    /// </summary>
    /// <param name="keyName">Name of constraint</param>
    public ManyToOnePart<TOther> UniqueKey(string keyName)
    {
      this.columnAttributes.Set("UniqueKey", 2, (object) keyName);
      return this;
    }

    /// <summary>
    /// Specifies the index name
    /// 
    /// </summary>
    /// <param name="indexName">Index name</param>
    public ManyToOnePart<TOther> Index(string indexName)
    {
      this.columnAttributes.Set("Index", 2, (object) indexName);
      return this;
    }

    /// <summary>
    /// Specifies the child class of this relationship
    /// 
    /// </summary>
    /// <typeparam name="T">Child</typeparam>
    public ManyToOnePart<TOther> Class<T>()
    {
      return this.Class(typeof (T));
    }

    /// <summary>
    /// Specifies the child class of this relationship
    /// 
    /// </summary>
    /// <param name="type">Child</param>
    public ManyToOnePart<TOther> Class(Type type)
    {
      this.attributes.Set("Class", 2, (object) new TypeReference(type));
      return this;
    }

    /// <summary>
    /// Sets this relationship to read-only
    /// 
    /// </summary>
    /// 
    /// <remarks>
    /// This is the same as calling both Not.Insert() and Not.Update()
    /// 
    /// </remarks>
    /// 
    /// <example>
    /// ReadOnly();
    ///             Not.ReadOnly();
    /// 
    /// </example>
    public ManyToOnePart<TOther> ReadOnly()
    {
      this.attributes.Set("Insert", 2, (object) (bool) (!this.nextBool ? 1 : 0));
      this.attributes.Set("Update", 2, (object) (bool) (!this.nextBool ? 1 : 0));
      this.nextBool = true;
      return this;
    }

    /// <summary>
    /// Specify the lazy behaviour of this relationship.
    /// 
    /// </summary>
    /// 
    /// <remarks>
    /// Defaults to Proxy lazy-loading. Use the <see cref="P:FluentNHibernate.Mapping.ManyToOnePart`1.Not"/> modifier to disable
    ///             lazy-loading, and use the <see cref="M:FluentNHibernate.Mapping.ManyToOnePart`1.LazyLoad(FluentNHibernate.Mapping.Laziness)"/>
    ///             overload to specify alternative lazy strategies.
    /// 
    /// </remarks>
    /// 
    /// <example>
    /// LazyLoad();
    ///             Not.LazyLoad();
    /// 
    /// </example>
    public ManyToOnePart<TOther> LazyLoad()
    {
      if (this.nextBool)
        this.LazyLoad(Laziness.Proxy);
      else
        this.LazyLoad(Laziness.False);
      this.nextBool = true;
      return this;
    }

    /// <summary>
    /// Specify the lazy behaviour of this relationship. Cannot be used
    ///             with the <see cref="P:FluentNHibernate.Mapping.ManyToOnePart`1.Not"/> modifier.
    /// 
    /// </summary>
    /// <param name="laziness">Laziness strategy</param>
    /// <example>
    /// LazyLoad(Laziness.NoProxy);
    /// 
    /// </example>
    public ManyToOnePart<TOther> LazyLoad(Laziness laziness)
    {
      this.attributes.Set("Lazy", 2, (object) laziness.ToString());
      this.nextBool = true;
      return this;
    }

    /// <summary>
    /// Specifies this relationship should be created with a default-named
    ///             foreign key.
    /// 
    /// </summary>
    public ManyToOnePart<TOther> ForeignKey()
    {
      return this.ForeignKey(string.Format("FK_{0}To{1}", (object) this.member.DeclaringType.Name, (object) this.member.Name));
    }

    /// <summary>
    /// Specifies the foreign-key constraint name
    /// 
    /// </summary>
    /// <param name="foreignKeyName">Constraint name</param>
    public ManyToOnePart<TOther> ForeignKey(string foreignKeyName)
    {
      this.attributes.Set("ForeignKey", 2, (object) foreignKeyName);
      return this;
    }

    /// <summary>
    /// Specifies that this relationship is insertable
    /// 
    /// </summary>
    public ManyToOnePart<TOther> Insert()
    {
      this.attributes.Set("Insert", 2, (object) (bool) (this.nextBool ? 1 : 0));
      this.nextBool = true;
      return this;
    }

    /// <summary>
    /// Specifies that this relationship is updatable
    /// 
    /// </summary>
    public ManyToOnePart<TOther> Update()
    {
      this.attributes.Set("Update", 2, (object) (bool) (this.nextBool ? 1 : 0));
      this.nextBool = true;
      return this;
    }

    /// <summary>
    /// Sets the single column used in this relationship. Use <see cref="M:FluentNHibernate.Mapping.ManyToOnePart`1.Columns(System.String[])"/>
    ///             if you need to specify more than one column.
    /// 
    /// </summary>
    /// <param name="name">Column name</param>
    public ManyToOnePart<TOther> Column(string name)
    {
      this.columns.Clear();
      this.columns.Add(name);
      return this;
    }

    /// <summary>
    /// Specifies the columns used in this relationship
    /// 
    /// </summary>
    /// <param name="newColumns">Columns</param>
    public ManyToOnePart<TOther> Columns(params string[] newColumns)
    {
      foreach (string str in newColumns)
        this.columns.Add(str);
      return this;
    }

    /// <summary>
    /// Specifies the columns used in this relationship
    /// 
    /// </summary>
    /// <param name="newColumns">Columns</param>
    public ManyToOnePart<TOther> Columns(params Expression<Func<TOther, object>>[] newColumns)
    {
      foreach (Expression<Func<TOther, object>> propertyExpression in newColumns)
        this.Columns(new string[1]
        {
          ReflectionExtensions.ToMember<TOther, object>(propertyExpression).Name
        });
      return this;
    }

    /// <summary>
    /// Specifies the sql formula used for this relationship
    /// 
    /// </summary>
    /// <param name="formula">Formula</param>
    public ManyToOnePart<TOther> Formula(string formula)
    {
      this.attributes.Set("Formula", 2, (object) formula);
      return this;
    }

    /// <summary>
    /// Specifies the property reference
    /// 
    /// </summary>
    /// <param name="expression">Property</param>
    public ManyToOnePart<TOther> PropertyRef(Expression<Func<TOther, object>> expression)
    {
      return this.PropertyRef(ReflectionExtensions.ToMember<TOther, object>(expression).Name);
    }

    /// <summary>
    /// Specifies the property reference
    /// 
    /// </summary>
    /// <param name="property">Property</param>
    public ManyToOnePart<TOther> PropertyRef(string property)
    {
      this.attributes.Set("PropertyRef", 2, (object) property);
      return this;
    }

    /// <summary>
    /// Sets this relationship to nullable
    /// 
    /// </summary>
    /// 
    /// <example>
    /// Nullable();
    ///             Not.Nullable();
    /// 
    /// </example>
    public ManyToOnePart<TOther> Nullable()
    {
      this.columnAttributes.Set("NotNull", 2, (object) (bool) (!this.nextBool ? 1 : 0));
      this.nextBool = true;
      return this;
    }

    /// <summary>
    /// Specifies an entity-name.
    /// 
    /// </summary>
    /// 
    /// <remarks>
    /// See http://nhforge.org/blogs/nhibernate/archive/2008/10/21/entity-name-in-action-a-strongly-typed-entity.aspx
    /// </remarks>
    public ManyToOnePart<TOther> EntityName(string entityName)
    {
      this.attributes.Set("EntityName", 2, (object) entityName);
      return this;
    }

    ManyToOneMapping IManyToOneMappingProvider.GetManyToOneMapping()
    {
      ManyToOneMapping manyToOneMapping1 = new ManyToOneMapping(this.attributes.Clone())
      {
        ContainingEntityType = this.entity,
        Member = this.member
      };
      ManyToOneMapping manyToOneMapping2 = manyToOneMapping1;
      ParameterExpression parameterExpression1 = Expression.Parameter(typeof (ManyToOneMapping), "x");
      // ISSUE: method reference
      Expression<Func<ManyToOneMapping, string>> expression1 = Expression.Lambda<Func<ManyToOneMapping, string>>((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ManyToOneMapping.get_Name))), new ParameterExpression[1]
      {
        parameterExpression1
      });
      int layer1 = 0;
      string name = this.member.Name;
      manyToOneMapping2.Set<string>(expression1, layer1, name);
      ManyToOneMapping manyToOneMapping3 = manyToOneMapping1;
      ParameterExpression parameterExpression2 = Expression.Parameter(typeof (ManyToOneMapping), "x");
      // ISSUE: method reference
      Expression<Func<ManyToOneMapping, TypeReference>> expression2 = Expression.Lambda<Func<ManyToOneMapping, TypeReference>>((Expression) Expression.Property((Expression) parameterExpression2, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ManyToOneMapping.get_Class))), new ParameterExpression[1]
      {
        parameterExpression2
      });
      int layer2 = 0;
      TypeReference typeReference = new TypeReference(typeof (TOther));
      manyToOneMapping3.Set<TypeReference>(expression2, layer2, typeReference);
      if (this.columns.Count == 0 && !manyToOneMapping1.IsSpecified("Formula"))
        manyToOneMapping1.AddColumn(0, this.CreateColumn(this.member.Name + "_id"));
      foreach (string column1 in (IEnumerable<string>) this.columns)
      {
        ColumnMapping column2 = this.CreateColumn(column1);
        manyToOneMapping1.AddColumn(2, column2);
      }
      return manyToOneMapping1;
    }

    private ColumnMapping CreateColumn(string column)
    {
      ColumnMapping columnMapping1 = new ColumnMapping(this.columnAttributes.Clone());
      ColumnMapping columnMapping2 = columnMapping1;
      ParameterExpression parameterExpression = Expression.Parameter(typeof (ColumnMapping), "x");
      // ISSUE: method reference
      Expression<Func<ColumnMapping, string>> expression = Expression.Lambda<Func<ColumnMapping, string>>((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ColumnMapping.get_Name))), new ParameterExpression[1]
      {
        parameterExpression
      });
      int layer = 0;
      string str = column;
      columnMapping2.Set<string>(expression, layer, str);
      return columnMapping1;
    }

    public ManyToOnePart<TOther> OptimisticLock()
    {
      this.attributes.Set("OptimisticLock", 2, (object) (bool) (this.nextBool ? 1 : 0));
      this.nextBool = true;
      return this;
    }
  }
}
