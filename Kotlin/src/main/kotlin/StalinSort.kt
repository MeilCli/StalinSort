fun main(args: Array<String>) {
    println("Hello Stalin!")
    writeStalinSort(intArrayOf(4))
    writeStalinSort(intArrayOf(6, 2, 5, 7, 3, 8, 8, 4))
    writeStalinSort(intArrayOf(5, 3, 7, 8, 9, 5, 3, 5, 7))
    /**
     * Hello Stalin!
     * Input: 4
     * StalinBy: 4
     * StalinByDescending: 4
     * Input: 6,2,5,7,3,8,8,4
     * StalinBy: 6,7,8,8
     * StalinByDescending: 6,2
     * Input: 5,3,7,8,9,5,3,5,7
     * StalinBy: 5,7,8,9
     * StalinByDescending: 5,3,3
     */
}

private fun writeStalinSort(source: IntArray) {
    println("Input: ${source.joinToString(",")}")
    println("StalinBy: ${source.asSequence().stalinBy().joinToString(",")}")
    println("StalinByDescending: ${source.asSequence().stalinByDescending().joinToString(",")}")
}

fun <T> Sequence<T>.stalinBy(): List<T> where T : Comparable<T> {
    return stalinSort(this, false)
}

fun <T> Sequence<T>.stalinByDescending(): List<T> where T : Comparable<T> {
    return stalinSort(this, true)
}

private fun <T> stalinSort(source: Sequence<T>, descending: Boolean): List<T> where T : Comparable<T> {
    val iterator = source.iterator()
    val result = mutableListOf<T>()

    if (iterator.hasNext()) {
        var lastElement = iterator.next()
        result.add(lastElement)

        while (iterator.hasNext()) {
            val element = iterator.next()
            val compare = when (descending) {
                true -> element <= lastElement
                false -> lastElement <= element
            }
            if (compare) {
                result.add(element)
                lastElement = element
            }
        }
    }

    return result
}