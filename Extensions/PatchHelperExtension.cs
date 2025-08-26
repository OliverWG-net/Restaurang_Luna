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

            //save props in dict key by name
            var entityProps = typeof(TEntity).GetProperties()
                .Where(p => p.CanWrite)
                .ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);

            //loop over dto
            foreach (var dtoProp in typeof(TPatch).GetProperties())
            {
                //read incoming value and skip if value is null
                var value = dtoProp.GetValue(patch);
                if (value == null)
                    continue;

                //if entity got a prop with matching name get it
                if (entityProps.TryGetValue(dtoProp.Name, out var entityProp))
                {
                    //skips if value is already matching
                    var current = entityProp.GetValue(entity);

                    if (Equals(current, value))
                        continue;

                    //assign new value
                    entityProp.SetValue(entity, value);
                    changed[entityProp.Name] = value;
                }
            }

            return changed;
        }
    }
}
