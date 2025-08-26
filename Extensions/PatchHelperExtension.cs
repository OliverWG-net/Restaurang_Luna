using System.Runtime.CompilerServices;

namespace Restaurang_luna.Extensions
{
    public static class PatchHelperExtension
    {
        public static Dictionary<string, object> PatchFrom<TPatch, TEntity>(this TEntity entity, TPatch patch)
        {
            var changed = new Dictionary<string, object>();

            if (entity == null || patch == null)
                return changed;

            var entityProps = typeof(TEntity).GetProperties()
                .Where(p => p.CanWrite)
                .ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);

            foreach (var dtoProp in typeof(TPatch).GetProperties())
            {
                var value = dtoProp.GetValue(patch);
                if (value == null)
                    continue;

                if (entityProps.TryGetValue(dtoProp.Name, out var entityProp))
                {
                    var current = entityProp.GetValue(entity);

                    if (Equals(current, value))
                        continue;

                    entityProp.SetValue(entity, value);
                    changed[entityProp.Name] = value;
                }
            }

            return changed;
        }
    }
}
