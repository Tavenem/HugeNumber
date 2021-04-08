using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavenem.HugeNumbers
{
    /// <summary>
    /// Extensions to <see cref="IEnumerable{T}"/> for the <see cref="HugeNumber"/> type.
    /// </summary>
    public static class HugeNumberEnumerableExtensions
    {
        /// <summary>
        /// Computes the average of a sequence of <see cref="HugeNumber"/> values.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="HugeNumber"/> values to calculate the average of.
        /// </param>
        /// <returns>The average of the sequence of values.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source"/> contains no elements.
        /// </exception>
        public static HugeNumber Average(this IEnumerable<HugeNumber> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (!source.Any())
            {
                throw new InvalidOperationException(HugeNumberErrorMessages.CollectionEmpty);
            }
            var sum = HugeNumber.Zero;
            var total = 0;
            foreach (var item in source)
            {
                sum += item;
                total++;
            }
            return sum / total;
        }

        /// <summary>
        /// Computes the average of a sequence of <see cref="HugeNumber"/> values.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="HugeNumber"/> values to calculate the average of.
        /// </param>
        /// <returns>The average of the sequence of values.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source"/> contains no elements.
        /// </exception>
        public static async ValueTask<HugeNumber> AverageAsync(this IAsyncEnumerable<HugeNumber> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            var any = false;
            var sum = HugeNumber.Zero;
            var total = 0;
            await foreach (var item in source)
            {
                any = true;
                sum += item;
                total++;
            }
            if (!any)
            {
                throw new InvalidOperationException(HugeNumberErrorMessages.CollectionEmpty);
            }
            return sum / total;
        }

        /// <summary>
        /// Computes the average of a sequence of nullable <see cref="HugeNumber"/> values.
        /// </summary>
        /// <param name="source">
        /// A sequence of nullable <see cref="HugeNumber"/> values to calculate the average of.
        /// </param>
        /// <returns>
        /// The average of the sequence of values, or null if the source sequence is empty or
        /// contains only values that are null.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static HugeNumber? Average(this IEnumerable<HugeNumber?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            var values = source.Where(x => x.HasValue).ToList();
            if (values.Count == 0)
            {
                return null;
            }
            return values.Sum() / values.Count;
        }

        /// <summary>
        /// Computes the average of a sequence of nullable <see cref="HugeNumber"/> values.
        /// </summary>
        /// <param name="source">
        /// A sequence of nullable <see cref="HugeNumber"/> values to calculate the average of.
        /// </param>
        /// <returns>
        /// The average of the sequence of values, or null if the source sequence is empty or
        /// contains only values that are null.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static async ValueTask<HugeNumber?> AverageAsync(this IAsyncEnumerable<HugeNumber?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            var sum = HugeNumber.Zero;
            var count = 0L;
            await foreach (var item in source)
            {
                if (item.HasValue)
                {
                    sum += item.Value;
                    count++;
                }
            }
            if (count == 0)
            {
                return null;
            }
            return sum / count;
        }

        /// <summary>
        /// Computes the average of a sequence of <see cref="HugeNumber"/> values that are obtained
        /// by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The average of the sequence of values.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="selector"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source"/> contains no elements.
        /// </exception>
        public static HugeNumber Average<TSource>(this IEnumerable<TSource> source, Func<TSource, HugeNumber> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            else
            {
                return Average(source.Select(x => selector(x)));
            }
        }

        /// <summary>
        /// Computes the average of a sequence of <see cref="HugeNumber"/> values that are obtained
        /// by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The average of the sequence of values.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="selector"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source"/> contains no elements.
        /// </exception>
        public static async ValueTask<HugeNumber> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, HugeNumber> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            var sum = HugeNumber.Zero;
            var count = 0L;
            await foreach (var item in source)
            {
                sum += selector(item);
                count++;
            }
            if (count == 0)
            {
                throw new InvalidOperationException(HugeNumberErrorMessages.CollectionEmpty);
            }
            return sum / count;
        }

        /// <summary>
        /// Computes the average of a sequence of <see cref="HugeNumber"/> values that are obtained
        /// by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The average of the sequence of values.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="selector"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source"/> contains no elements.
        /// </exception>
        public static async ValueTask<HugeNumber> AverageAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<HugeNumber>> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            var sum = HugeNumber.Zero;
            var count = 0L;
            await foreach (var item in source)
            {
                sum += await selector(item).ConfigureAwait(false);
                count++;
            }
            if (count == 0)
            {
                throw new InvalidOperationException(HugeNumberErrorMessages.CollectionEmpty);
            }
            return sum / count;
        }

        /// <summary>
        /// Computes the average of a sequence of nullable <see cref="HugeNumber"/> values that are
        /// obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>
        /// The average of the sequence of values, or null if the source sequence is empty or
        /// contains only values that are null.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="selector"/> is null.
        /// </exception>
        public static HugeNumber? Average<TSource>(this IEnumerable<TSource> source, Func<TSource, HugeNumber?> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            else
            {
                return Average(source.Select(x => selector(x)));
            }
        }

        /// <summary>
        /// Computes the average of a sequence of nullable <see cref="HugeNumber"/> values that are
        /// obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>
        /// The average of the sequence of values, or null if the source sequence is empty or
        /// contains only values that are null.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="selector"/> is null.
        /// </exception>
        public static async ValueTask<HugeNumber?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, HugeNumber?> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            var sum = HugeNumber.Zero;
            var count = 0L;
            await foreach (var item in source)
            {
                var value = selector(item);
                if (value.HasValue)
                {
                    sum += value.Value;
                    count++;
                }
            }
            if (count == 0)
            {
                return null;
            }
            return sum / count;
        }

        /// <summary>
        /// Computes the average of a sequence of nullable <see cref="HugeNumber"/> values that are
        /// obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>
        /// The average of the sequence of values, or null if the source sequence is empty or
        /// contains only values that are null.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="selector"/> is null.
        /// </exception>
        public static async ValueTask<HugeNumber?> AverageAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<HugeNumber?>> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            var sum = HugeNumber.Zero;
            var count = 0L;
            await foreach (var item in source)
            {
                var value = await selector(item).ConfigureAwait(false);
                if (value.HasValue)
                {
                    sum += value.Value;
                    count++;
                }
            }
            if (count == 0)
            {
                return null;
            }
            return sum / count;
        }

        /// <summary>
        /// Returns the maximum value in a sequence of <see cref="HugeNumber"/> values.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="HugeNumber"/> values to determine the maximum value of.
        /// </param>
        /// <returns>The maximum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source"/> contains no elements.
        /// </exception>
        public static HugeNumber Max(this IEnumerable<HugeNumber> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (!source.Any())
            {
                throw new InvalidOperationException(HugeNumberErrorMessages.CollectionEmpty);
            }
            var max = HugeNumber.NegativeInfinity;
            foreach (var item in source)
            {
                max = HugeNumber.Max(max, item);
            }
            return max;
        }

        /// <summary>
        /// Returns the maximum value in a sequence of <see cref="HugeNumber"/> values.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="HugeNumber"/> values to determine the maximum value of.
        /// </param>
        /// <returns>The maximum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source"/> contains no elements.
        /// </exception>
        public static async ValueTask<HugeNumber> MaxAsync(this IAsyncEnumerable<HugeNumber> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            var any = false;
            var max = HugeNumber.NegativeInfinity;
            await foreach (var item in source)
            {
                any = true;
                max = HugeNumber.Max(max, item);
            }
            if (!any)
            {
                throw new InvalidOperationException(HugeNumberErrorMessages.CollectionEmpty);
            }
            return max;
        }

        /// <summary>
        /// Returns the maximum value in a sequence of nullable <see cref="HugeNumber"/> values.
        /// </summary>
        /// <param name="source">
        /// A sequence of nullable <see cref="HugeNumber"/> values to determine the maximum value of.
        /// </param>
        /// <returns>The maximum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static HugeNumber? Max(this IEnumerable<HugeNumber?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (!source.Any())
            {
                throw new InvalidOperationException(HugeNumberErrorMessages.CollectionEmpty);
            }
            HugeNumber? max = null;
            foreach (var item in source.Where(x => x.HasValue).Select(x => x!.Value))
            {
                if (max.HasValue)
                {
                    max = HugeNumber.Max(max.Value, item);
                }
                else
                {
                    max = item;
                }
            }
            return max;
        }

        /// <summary>
        /// Returns the maximum value in a sequence of nullable <see cref="HugeNumber"/> values.
        /// </summary>
        /// <param name="source">
        /// A sequence of nullable <see cref="HugeNumber"/> values to determine the maximum value of.
        /// </param>
        /// <returns>The maximum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static async ValueTask<HugeNumber?> MaxAsync(this IAsyncEnumerable<HugeNumber?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            HugeNumber? max = null;
            await foreach (var item in source)
            {
                if (!item.HasValue)
                {
                    continue;
                }
                if (max.HasValue)
                {
                    max = HugeNumber.Max(max.Value, item.Value);
                }
                else
                {
                    max = item.Value;
                }
            }
            return max;
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the maximum <see
        /// cref="HugeNumber"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence of values to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="selector"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source"/> contains no elements.
        /// </exception>
        public static HugeNumber Max<TSource>(this IEnumerable<TSource> source, Func<TSource, HugeNumber> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            else
            {
                return Max(source.Select(x => selector(x)));
            }
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the maximum <see
        /// cref="HugeNumber"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence of values to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="selector"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source"/> contains no elements.
        /// </exception>
        public static async ValueTask<HugeNumber> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, HugeNumber> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            var any = false;
            var max = HugeNumber.NegativeInfinity;
            await foreach (var item in source)
            {
                any = true;
                max = HugeNumber.Max(max, selector(item));
            }
            if (!any)
            {
                throw new InvalidOperationException(HugeNumberErrorMessages.CollectionEmpty);
            }
            return max;
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the maximum <see
        /// cref="HugeNumber"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence of values to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="selector"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source"/> contains no elements.
        /// </exception>
        public static async ValueTask<HugeNumber> MaxAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<HugeNumber>> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            var any = false;
            var max = HugeNumber.NegativeInfinity;
            await foreach (var item in source)
            {
                any = true;
                max = HugeNumber.Max(max, await selector(item).ConfigureAwait(false));
            }
            if (!any)
            {
                throw new InvalidOperationException(HugeNumberErrorMessages.CollectionEmpty);
            }
            return max;
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the maximum
        /// nullable <see cref="HugeNumber"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence of values to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="selector"/> is null.
        /// </exception>
        public static HugeNumber? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, HugeNumber?> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            else
            {
                return Max(source.Select(x => selector(x)));
            }
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the maximum
        /// nullable <see cref="HugeNumber"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence of values to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="selector"/> is null.
        /// </exception>
        public static async ValueTask<HugeNumber?> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, HugeNumber?> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            HugeNumber? max = null;
            await foreach (var item in source)
            {
                var value = selector(item);
                if (!value.HasValue)
                {
                    continue;
                }
                if (max.HasValue)
                {
                    max = HugeNumber.Max(max.Value, value.Value);
                }
                else
                {
                    max = value.Value;
                }
            }
            return max;
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the maximum
        /// nullable <see cref="HugeNumber"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence of values to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="selector"/> is null.
        /// </exception>
        public static async ValueTask<HugeNumber?> MaxAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<HugeNumber?>> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            HugeNumber? max = null;
            await foreach (var item in source)
            {
                var value = await selector(item).ConfigureAwait(false);
                if (!value.HasValue)
                {
                    continue;
                }
                if (max.HasValue)
                {
                    max = HugeNumber.Max(max.Value, value.Value);
                }
                else
                {
                    max = value.Value;
                }
            }
            return max;
        }

        /// <summary>
        /// Returns the minimum value in a sequence of <see cref="HugeNumber"/> values.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="HugeNumber"/> values to determine the minimum value of.
        /// </param>
        /// <returns>The minimum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source"/> contains no elements.
        /// </exception>
        public static HugeNumber Min(this IEnumerable<HugeNumber> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (!source.Any())
            {
                throw new InvalidOperationException(HugeNumberErrorMessages.CollectionEmpty);
            }
            var min = HugeNumber.PositiveInfinity;
            foreach (var item in source)
            {
                min = HugeNumber.Min(min, item);
            }
            return min;
        }

        /// <summary>
        /// Returns the minimum value in a sequence of <see cref="HugeNumber"/> values.
        /// </summary>
        /// <param name="source">
        /// A sequence of <see cref="HugeNumber"/> values to determine the minimum value of.
        /// </param>
        /// <returns>The minimum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source"/> contains no elements.
        /// </exception>
        public static async ValueTask<HugeNumber> MinAsync(this IAsyncEnumerable<HugeNumber> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            var any = false;
            var min = HugeNumber.PositiveInfinity;
            await foreach (var item in source)
            {
                any = true;
                min = HugeNumber.Min(min, item);
            }
            if (!any)
            {
                throw new InvalidOperationException(HugeNumberErrorMessages.CollectionEmpty);
            }
            return min;
        }

        /// <summary>
        /// Returns the minimum value in a sequence of nullable <see cref="HugeNumber"/> values.
        /// </summary>
        /// <param name="source">
        /// A sequence of nullable <see cref="HugeNumber"/> values to determine the minimum value of.
        /// </param>
        /// <returns>The minimum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static HugeNumber? Min(this IEnumerable<HugeNumber?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (!source.Any())
            {
                throw new InvalidOperationException(HugeNumberErrorMessages.CollectionEmpty);
            }
            HugeNumber? min = null;
            foreach (var item in source.Where(x => x.HasValue).Select(x => x!.Value))
            {
                if (min.HasValue)
                {
                    min = HugeNumber.Min(min.Value, item);
                }
            }
            return min;
        }

        /// <summary>
        /// Returns the minimum value in a sequence of nullable <see cref="HugeNumber"/> values.
        /// </summary>
        /// <param name="source">
        /// A sequence of nullable <see cref="HugeNumber"/> values to determine the minimum value of.
        /// </param>
        /// <returns>The minimum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static async ValueTask<HugeNumber?> MinAsync(this IAsyncEnumerable<HugeNumber?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            HugeNumber? min = null;
            await foreach (var item in source)
            {
                if (!item.HasValue)
                {
                    continue;
                }
                if (min.HasValue)
                {
                    min = HugeNumber.Min(min.Value, item.Value);
                }
            }
            return min;
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the minimum <see
        /// cref="HugeNumber"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence of values to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="selector"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source"/> contains no elements.
        /// </exception>
        public static HugeNumber Min<TSource>(this IEnumerable<TSource> source, Func<TSource, HugeNumber> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            else
            {
                return Min(source.Select(x => selector(x)));
            }
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the minimum <see
        /// cref="HugeNumber"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence of values to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="selector"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source"/> contains no elements.
        /// </exception>
        public static async ValueTask<HugeNumber> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, HugeNumber> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            var any = false;
            var min = HugeNumber.PositiveInfinity;
            await foreach (var item in source)
            {
                any = true;
                min = HugeNumber.Min(min, selector(item));
            }
            if (!any)
            {
                throw new InvalidOperationException(HugeNumberErrorMessages.CollectionEmpty);
            }
            return min;
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the minimum <see
        /// cref="HugeNumber"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence of values to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="selector"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="source"/> contains no elements.
        /// </exception>
        public static async ValueTask<HugeNumber> MinAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<HugeNumber>> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            var any = false;
            var min = HugeNumber.PositiveInfinity;
            await foreach (var item in source)
            {
                any = true;
                min = HugeNumber.Min(min, await selector(item).ConfigureAwait(false));
            }
            if (!any)
            {
                throw new InvalidOperationException(HugeNumberErrorMessages.CollectionEmpty);
            }
            return min;
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the minimum
        /// nullable <see cref="HugeNumber"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence of values to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="selector"/> is null.
        /// </exception>
        public static HugeNumber? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, HugeNumber?> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            else
            {
                return Min(source.Select(x => selector(x)));
            }
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the minimum
        /// nullable <see cref="HugeNumber"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence of values to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="selector"/> is null.
        /// </exception>
        public static async ValueTask<HugeNumber?> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, HugeNumber?> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            HugeNumber? min = null;
            await foreach (var item in source)
            {
                var value = selector(item);
                if (!value.HasValue)
                {
                    continue;
                }
                if (min.HasValue)
                {
                    min = HugeNumber.Min(min.Value, value.Value);
                }
            }
            return min;
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the minimum
        /// nullable <see cref="HugeNumber"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence of values to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="selector"/> is null.
        /// </exception>
        public static async ValueTask<HugeNumber?> MinAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<HugeNumber?>> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            HugeNumber? min = null;
            await foreach (var item in source)
            {
                var value = await selector(item).ConfigureAwait(false);
                if (!value.HasValue)
                {
                    continue;
                }
                if (min.HasValue)
                {
                    min = HugeNumber.Min(min.Value, value.Value);
                }
            }
            return min;
        }

        /// <summary>
        /// Bypasses a specified number of elements in a sequence and then returns the remaining elements.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence of values.</param>
        /// <param name="count">The number of elements to skip before returning the remaining elements.</param>
        /// <returns>The sequence, after skipping the specified number of elements.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="count"/> is <see cref="HugeNumber.NaN"/>.
        /// </exception>
        public static IEnumerable<TSource> Skip<TSource>(this IEnumerable<TSource> source, HugeNumber count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (count.IsNaN)
            {
                throw new ArgumentException(HugeNumberErrorMessages.NaNInvalid, nameof(count));
            }

            if (count.IsNegativeInfinity)
            {
                return source;
            }
            if (count.IsPositiveInfinity)
            {
                return Enumerable.Empty<TSource>();
            }

            return Enumerable.Skip(source, (int)count);
        }

        /// <summary>
        /// Bypasses a specified number of elements in a sequence and then returns the remaining elements.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence of values.</param>
        /// <param name="count">The number of elements to skip before returning the remaining elements.</param>
        /// <returns>The sequence, after skipping the specified number of elements.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="count"/> is <see cref="HugeNumber.NaN"/>.
        /// </exception>
        public static IAsyncEnumerable<TSource> Skip<TSource>(this IAsyncEnumerable<TSource> source, HugeNumber count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (count.IsNaN)
            {
                throw new ArgumentException(HugeNumberErrorMessages.NaNInvalid, nameof(count));
            }

            if (count.IsNegativeInfinity)
            {
                return source;
            }
            if (count.IsPositiveInfinity)
            {
                return EmptyAsyncEnumerable<TSource>();
            }

            return SkipInternal();

            async IAsyncEnumerable<TSource> SkipInternal()
            {
                var skipped = HugeNumber.Zero;
                await foreach (var item in source)
                {
                    if (skipped >= count)
                    {
                        yield return item;
                    }
                    else
                    {
                        skipped++;
                    }
                }
            }
        }

        /// <summary>
        /// Computes the sum of a sequence of <see cref="HugeNumber"/> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="HugeNumber"/> values to calculate the sum of.</param>
        /// <returns>The sum of the values in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        public static HugeNumber Sum(this IEnumerable<HugeNumber> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var sum = HugeNumber.Zero;
            foreach (var item in source)
            {
                sum += item;
            }
            return sum;
        }

        /// <summary>
        /// Computes the sum of a sequence of <see cref="HugeNumber"/> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="HugeNumber"/> values to calculate the sum of.</param>
        /// <returns>The sum of the values in the sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        public static async ValueTask<HugeNumber> SumAsync(this IAsyncEnumerable<HugeNumber> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var sum = HugeNumber.Zero;
            await foreach (var item in source)
            {
                sum += item;
            }
            return sum;
        }

        /// <summary>
        /// Computes the sum of a sequence of nullable <see cref="HugeNumber"/> values.
        /// </summary>
        /// <param name="source">
        /// A sequence of nullable <see cref="HugeNumber"/> values to calculate the sum of.
        /// </param>
        /// <returns>The sum of the values in the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static HugeNumber? Sum(this IEnumerable<HugeNumber?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var values = source.Where(x => x.HasValue).Select(x => x!.Value);
            return values.Any()
                ? values.Sum()
                : (HugeNumber?)null;
        }

        /// <summary>
        /// Computes the sum of a sequence of nullable <see cref="HugeNumber"/> values.
        /// </summary>
        /// <param name="source">
        /// A sequence of nullable <see cref="HugeNumber"/> values to calculate the sum of.
        /// </param>
        /// <returns>The sum of the values in the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static async ValueTask<HugeNumber?> SumAsync(this IAsyncEnumerable<HugeNumber?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            HugeNumber? sum = null;
            await foreach (var item in source)
            {
                if (item.HasValue)
                {
                    if (sum.HasValue)
                    {
                        sum = sum.Value + item.Value;
                    }
                    else
                    {
                        sum = item;
                    }
                }
            }
            return sum;
        }

        /// <summary>
        /// Computes the sum of the sequence of <see cref="HugeNumber"/> values that are obtained by
        /// invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The sum of the projected values.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="selector"/> is null.
        /// </exception>
        public static HugeNumber Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, HugeNumber> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            else
            {
                return Sum(source.Select(x => selector(x)));
            }
        }

        /// <summary>
        /// Computes the sum of the sequence of <see cref="HugeNumber"/> values that are obtained by
        /// invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The sum of the projected values.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="selector"/> is null.
        /// </exception>
        public static async ValueTask<HugeNumber> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, HugeNumber> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            var sum = HugeNumber.Zero;
            await foreach (var item in source)
            {
                sum += selector(item);
            }
            return sum;
        }

        /// <summary>
        /// Computes the sum of the sequence of <see cref="HugeNumber"/> values that are obtained by
        /// invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The sum of the projected values.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="selector"/> is null.
        /// </exception>
        public static async ValueTask<HugeNumber> SumAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<HugeNumber>> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            var sum = HugeNumber.Zero;
            await foreach (var item in source)
            {
                sum += await selector(item).ConfigureAwait(false);
            }
            return sum;
        }

        /// <summary>
        /// Computes the sum of the sequence of nullable <see cref="HugeNumber"/> values that are obtained by
        /// invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The sum of the projected values.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="selector"/> is null.
        /// </exception>
        public static HugeNumber? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, HugeNumber?> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            else
            {
                return Sum(source.Select(x => selector(x)));
            }
        }

        /// <summary>
        /// Computes the sum of the sequence of nullable <see cref="HugeNumber"/> values that are obtained by
        /// invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The sum of the projected values.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="selector"/> is null.
        /// </exception>
        public static async ValueTask<HugeNumber?> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, HugeNumber?> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            HugeNumber? sum = null;
            await foreach (var item in source)
            {
                var value = selector(item);
                if (value.HasValue)
                {
                    if (sum.HasValue)
                    {
                        sum = sum.Value + value.Value;
                    }
                    else
                    {
                        sum = value;
                    }
                }
            }
            return sum;
        }

        /// <summary>
        /// Computes the sum of the sequence of nullable <see cref="HugeNumber"/> values that are obtained by
        /// invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The sum of the projected values.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="selector"/> is null.
        /// </exception>
        public static async ValueTask<HugeNumber?> SumAwaitAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<HugeNumber?>> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            HugeNumber? sum = null;
            await foreach (var item in source)
            {
                var value = await selector(item).ConfigureAwait(false);
                if (value.HasValue)
                {
                    if (sum.HasValue)
                    {
                        sum = sum.Value + value.Value;
                    }
                    else
                    {
                        sum = value;
                    }
                }
            }
            return sum;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        private static async IAsyncEnumerable<TSource> EmptyAsyncEnumerable<TSource>()
        {
            yield break;
        }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    }
}
