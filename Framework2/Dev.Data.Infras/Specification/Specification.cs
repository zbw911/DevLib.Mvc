﻿// ***********************************************************************************
// Created by zbw911 
// 创建于：2013年01月30日 11:16
// 
// 修改于：2013年02月05日 17:31
// 文件名：Specification.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

namespace Dev.Data.Infras.Specification
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Dev.Data.Infras.Extensions;

    /// <summary>
    ///     The specification.
    /// </summary>
    /// <typeparam name="TEntity">
    /// </typeparam>
    public class Specification<TEntity> : ISpecification<TEntity>
    {
        #region Fields

        /// <summary>
        ///     The predicate.
        /// </summary>
        public Expression<Func<TEntity, bool>> Predicate;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Specification{TEntity}" /> class.
        /// </summary>
        /// <param name="predicate">
        ///     The predicate.
        /// </param>
        public Specification(Expression<Func<TEntity, bool>> predicate)
        {
            this.Predicate = predicate;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The and.
        /// </summary>
        /// <param name="specification">
        ///     The specification.
        /// </param>
        /// <returns>
        ///     The <see cref="Specification" />.
        /// </returns>
        public Specification<TEntity> And(Specification<TEntity> specification)
        {
            return new Specification<TEntity>(this.Predicate.And(specification.Predicate));
        }

        /// <summary>
        ///     The and.
        /// </summary>
        /// <param name="predicate">
        ///     The predicate.
        /// </param>
        /// <returns>
        ///     The <see cref="Specification" />.
        /// </returns>
        public Specification<TEntity> And(Expression<Func<TEntity, bool>> predicate)
        {
            return new Specification<TEntity>(this.Predicate.And(predicate));
        }

        /// <summary>
        ///     The or.
        /// </summary>
        /// <param name="specification">
        ///     The specification.
        /// </param>
        /// <returns>
        ///     The <see cref="Specification" />.
        /// </returns>
        public Specification<TEntity> Or(Specification<TEntity> specification)
        {
            return new Specification<TEntity>(this.Predicate.Or(specification.Predicate));
        }

        /// <summary>
        ///     The or.
        /// </summary>
        /// <param name="predicate">
        ///     The predicate.
        /// </param>
        /// <returns>
        ///     The <see cref="Specification" />.
        /// </returns>
        public Specification<TEntity> Or(Expression<Func<TEntity, bool>> predicate)
        {
            return new Specification<TEntity>(this.Predicate.Or(predicate));
        }

        /// <summary>
        ///     The satisfying entities from.
        /// </summary>
        /// <param name="query">
        ///     The query.
        /// </param>
        /// <returns>
        ///     The <see cref="IQueryable" />.
        /// </returns>
        public IQueryable<TEntity> SatisfyingEntitiesFrom(IQueryable<TEntity> query)
        {
            return query.Where(this.Predicate);
        }

        /// <summary>
        ///     The satisfying entity from.
        /// </summary>
        /// <param name="query">
        ///     The query.
        /// </param>
        /// <returns>
        ///     The <see cref="TEntity" />.
        /// </returns>
        public TEntity SatisfyingEntityFrom(IQueryable<TEntity> query)
        {
            return query.Where(this.Predicate).SingleOrDefault();
        }

        #endregion
    }
}