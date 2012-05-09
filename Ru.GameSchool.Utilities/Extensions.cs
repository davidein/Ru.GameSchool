using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;

namespace Ru.GameSchool.Utilities
{
    public static class Extensions
    {
        public static void SetAllModified<T>(T entity, ObjectContext context) where T : IEntityWithKey
        {
            var stateEntity = context.ObjectStateManager.GetObjectStateEntry(entity.EntityKey);
            var propertyNameList = stateEntity.CurrentValues.DataRecordInfo.FieldMetadata.Select(pn => pn.FieldType.Name);


            foreach (var propName in propertyNameList)
            {
                stateEntity.SetModifiedProperty(propName);
            }


        }
        public static void Update<T>(T entity, ObjectContext context) where T : IEntityWithKey
        {
            context.Attach(entity);
            context.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
            context.SaveChanges();
        }
    }
}
