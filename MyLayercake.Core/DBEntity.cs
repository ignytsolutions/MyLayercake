using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyLayercake.Core {
    public abstract class DBEntity : IEntity {
        private Guid _oid;
        private DateTime _created;

        [Key]
        public Guid Oid {
            get {
                if (_oid == null)
                    _oid = Guid.NewGuid();
                return _oid;
            }
            set {
                _oid = value;
            }
        }

        public DateTime Created {
            get {
                if (_created == null)
                    _created = DateTime.Now;
                return _created;
            }
            set {
                _created = value;
            }
        }
    }
}
