import { type MotionProps, m } from "framer-motion";

import { varContainer } from "./variants/container";

interface Props extends MotionProps {
	className?: string;
}

/**
 * Motion universal container
 *
 * variants: [Variants can be used to animate an entire subtree of a component using a single animation prop](https://www.framer.com/motion/animation/#variants)
 *
 * Variants is a set of predefined objects
 * const variants = {
 *   visible: { opacity: 1 },
 *   hidden: { opacity: 0 },
 * }
 *
 * Need to specify inital and animate attribute names
 * <motion.div
 *  initial="hidden"
 *  animate="visible"
 *  variants={variants}
 * />
 */
export default function MotionContainer({ children, className }: Props) {
	return (
		<m.div
			// After specifying the attribute names of initial, animate and exit here, the subcomponent does not need to be specified again.
			initial="initial"
			animate="animate"
			exit="exit"
			variants={varContainer()}
			className={className}
		>
			{children}
		</m.div>
	);
}
