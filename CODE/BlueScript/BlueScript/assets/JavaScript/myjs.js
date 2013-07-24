﻿function init() {
    var stage = new createjs.Stage("workarea");
    var circle = new createjs.Shape();
    circle.graphics.beginFill("red").drawCircle(0,0,50);
    circle.x = 100;
    circle.y = 100;
    stage.addChild(circle);
    // Short code way:
    // stage.addChild(new createjs.Shape()).setTransform(100,100).graphics.f("red").dc(0,0,50);
    stage.update();
}