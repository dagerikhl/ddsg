using System;

namespace DdSG {

    public class SelectedAction {

        public ActionType ActionType;
        public string Label;
        public string Description;
        public Action Action;

        public SelectedAction(ActionType type, Action action) {
            switch (type) {
            case ActionType.Sell:
                ActionType = type;
                Label = "Sell";
                Description = string.Format("Sell for {0:P0} of its orignal worth.", Constants.SELL_PERCENTAGE);
                Action = action;
                break;
            default:
                throw new ArgumentOutOfRangeException("type", type, "The action type doesn't exist.");
            }
        }

    }

}
