extends Control


# Declare member variables here. Examples:
# var a = 2
# var b = "text"
var startScene = preload("res://Visual Novel/UserInterface.tscn")

func _button_pressed():
	#warning-ignore:return_value_discarded
	get_tree().change_scene_to(startScene)
