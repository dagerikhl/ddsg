using System;

namespace DdSG {

    public class SelectedAction {

        public readonly ActionType ActionType;
        public readonly string Label;
        public readonly string Description;
        public readonly Action Action;

        public SelectedAction(ActionType type, Action action) {
            ActionType = type;
            Action = action;

            switch (type) {
            case ActionType.OpenExternalReferences:
                Label = "Open";
                Description = "Opens the external references related to this entity.";
                break;
            case ActionType.Sell:
                Label = "Sell";
                Description = string.Format("Sell for {0:P0} of its orignal worth.", Constants.SELL_PERCENTAGE);
                Action = () => {
                    if (!GameManager.IsQuickPaused) {
                        action();
                    }
                };
                break;
            default:
                throw new ArgumentOutOfRangeException("type", type, "The action type doesn't exist.");
            }
        }

    }

}
