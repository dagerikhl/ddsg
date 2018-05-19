using System;

namespace DdSG {

    public class SelectedAction {

        public ActionType ActionType;
        public string Label;
        public Action Action;

        public SelectedAction(ActionType type, Action action) {
            switch (type) {
            case ActionType.Sell:
                ActionType = type;
                Label = "Sell";
                Action = action;
                break;
            default:
                throw new ArgumentOutOfRangeException("type", type, "The action type doesn't exist.");
            }
        }

    }

}
