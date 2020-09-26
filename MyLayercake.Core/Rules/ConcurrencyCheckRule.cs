using MyLayercake.Core.Extensions;

namespace MyLayercake.Core.Rules {

    public class ConcurrencyCheckRule : RuleBase {
        private readonly IVersionContainer _originalEntity;
        private readonly IVersionContainer _newEntity;

        public ConcurrencyCheckRule(IVersionContainer originalEntity, IVersionContainer newEntity) {
            _originalEntity = originalEntity;
            _newEntity = newEntity;
        }

        protected override void OnValidate() {
            if (_originalEntity.Version != _newEntity.Version) {
                Invalidate($"{_newEntity.ClassName()} was changed by another user and cannot be changed.");
            }
        }
    }
}
