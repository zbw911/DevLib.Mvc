﻿// ***********************************************************************************
// Created by zbw911 
// 创建于：2013年01月30日 11:16
// 
// 修改于：2013年02月05日 17:31
// 文件名：ExpressionExtension.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

namespace Dev.Data.Infras.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    ///     http://blogs.msdn.com/b/meek/archive/2008/05/02/linq-to-entities-combining-predicates.aspx
    /// </summary>
    public static class ExpressionExtension
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The and.
        /// </summary>
        /// <param name="first">
        ///     The first.
        /// </param>
        /// <param name="second">
        ///     The second.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="Expression" />.
        /// </returns>
        public static Expression<Func<T, bool>> And<T>(
            this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.And);
        }

        /// <summary>
        ///     The compose.
        /// </summary>
        /// <param name="first">
        ///     The first.
        /// </param>
        /// <param name="second">
        ///     The second.
        /// </param>
        /// <param name="merge">
        ///     The merge.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="Expression" />.
        /// </returns>
        public static Expression<T> Compose<T>(
            this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            // build parameter map (from parameters of second to parameters of first)
            Dictionary<ParameterExpression, ParameterExpression> map =
                first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with parameters from the first
            Expression secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // apply composition of lambda expression bodies to parameters from the first expression 
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        /// <summary>
        ///     The or.
        /// </summary>
        /// <param name="first">
        ///     The first.
        /// </param>
        /// <param name="second">
        ///     The second.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="Expression" />.
        /// </returns>
        public static Expression<Func<T, bool>> Or<T>(
            this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.Or);
        }

        #endregion
    }
}